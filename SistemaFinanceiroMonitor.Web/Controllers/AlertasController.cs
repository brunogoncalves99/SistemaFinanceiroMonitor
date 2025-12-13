using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaFinanceiroMonitor.Application.DTOs;
using SistemaFinanceiroMonitor.Application.Interface;
using SistemaFinanceiroMonitor.Domain.Enums;
using SistemaFinanceiroMonitor.Web.Models;

namespace SistemaFinanceiroMonitor.Web.Controllers
{
    public class AlertasController : Controller
    {
        private readonly IAlertaService _alertaService;
        private readonly IUsuarioService _usuarioService;

        public AlertasController(IAlertaService alertaService, IUsuarioService usuarioService)
        {
            _alertaService = alertaService;
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index(int? usuarioId)
        {
            // Por enquanto, usar usuário fixo ID = 1
            // Depois implementar autenticação real

            var idUsuario = usuarioId ?? 1;

            var alertas = await _alertaService.ObterPorUsuarioAsync(idUsuario);

            var viewModel = new AlertaViewModel
            {
                UsuarioId = idUsuario,
                Alertas = alertas.ToList()
            };

            return View(viewModel);
        }

        public IActionResult Criar()
        {
            var viewModel = new CriarAlertaViewModel
            {
                UsuarioId = 1, 
                TiposAlerta = new SelectList(Enum.GetValues(typeof(TipoAlerta))),
                TiposMoeda = new SelectList(Enum.GetValues(typeof(TipoMoeda))),
                TiposIndicador = new SelectList(Enum.GetValues(typeof(TipoIndicador)))
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(CriarAlertaViewModel viewModel)
        {
            try
            {
                var dto = new CriarAlertaDTO
                {
                    UsuarioId = viewModel.UsuarioId,
                    TipoAlerta = viewModel.TipoAlerta,
                    TipoMoeda = viewModel.TipoMoeda,
                    TipoIndicador = viewModel.TipoIndicador,
                    ValorGatilho = viewModel.ValorGatilho,
                    PercentualGatilho = viewModel.PercentualGatilho,
                    Descricao = viewModel.Descricao
                };

                await _alertaService.CriarAlertaAsync(dto);

                TempData["Sucesso"] = "Alerta criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao criar alerta: {ex.Message}");
                viewModel.TiposAlerta = new SelectList(Enum.GetValues(typeof(TipoAlerta)));
                viewModel.TiposMoeda = new SelectList(Enum.GetValues(typeof(TipoMoeda)));
                viewModel.TiposIndicador = new SelectList(Enum.GetValues(typeof(TipoIndicador)));
                return View(viewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Pausar(int id)
        {
            try
            {
                await _alertaService.PausarAlertaAsync(id);
                TempData["Sucesso"] = "Alerta pausado com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["Erro"] = $"Erro ao pausar alerta: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Reativar(int id)
        {
            try
            {
                await _alertaService.ReativarAlertaAsync(id);
                TempData["Sucesso"] = "Alerta reativado com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["Erro"] = $"Erro ao reativar alerta: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                await _alertaService.RemoverAlertaAsync(id);
                TempData["Sucesso"] = "Alerta removido com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["Erro"] = $"Erro ao remover alerta: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Historico(int id)
        {
            var alerta = await _alertaService.ObterPorIdAsync(id);
            var historico = await _alertaService.ObterHistoricoAlertasAsync(id);

            var viewModel = new HistoricoAlertaViewModel
            {
                Alerta = alerta,
                Historico = historico.ToList()
            };

            return View(viewModel);
        }
    }
}