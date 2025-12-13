$(document).ready(function () {
    console.log("Sistema Financeiro Monitor - Carregado!");

    setTimeout(function () {
        $('.alert').fadeOut('slow');
    }, 5000);

    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    $('.btn-danger[data-confirm]').on('click', function (e) {
        if (!confirm($(this).data('confirm'))) {
            e.preventDefault();
        }
    });

    $('.currency-input').on('blur', function () {
        var value = parseFloat($(this).val());
        if (!isNaN(value)) {
            $(this).val(value.toFixed(2));
        }
    });

    $(document).ajaxStart(function () {
        $('#loading').show();
    }).ajaxStop(function () {
        $('#loading').hide();
    });
});

function formatarMoeda(valor) {
    return 'R$ ' + valor.toFixed(4).replace('.', ',');
}

function formatarData(data) {
    var d = new Date(data);
    var dia = String(d.getDate()).padStart(2, '0');
    var mes = String(d.getMonth() + 1).padStart(2, '0');
    var ano = d.getFullYear();
    return dia + '/' + mes + '/' + ano;
}

function formatarPercentual(valor) {
    var sinal = valor >= 0 ? '+' : '';
    return sinal + valor.toFixed(2) + '%';
}

function getVariacaoClass(valor) {
    return valor >= 0 ? 'text-success' : 'text-danger';
}

function getVariacaoIcone(valor) {
    return valor >= 0 ? 'fa-arrow-up' : 'fa-arrow-down';
}

function atualizarCotacoes(tipoMoeda) {
    $.ajax({
        url: '/api/CotacoesApi/Ultima/' + tipoMoeda,
        method: 'GET',
        success: function (data) {
            console.log('Cotação atualizada:', data);
        },
        error: function (error) {
            console.error('Erro ao obter cotação:', error);
            alert('Erro ao carregar cotações. Tente novamente.');
        }
    });
}

function atualizarDashboard() {
    $.ajax({
        url: '/api/DashboardApi/Dados',
        method: 'GET',
        success: function (data) {
            console.log('Dashboard atualizado:', data);
            // Processar dados e atualizar gráficos
        },
        error: function (error) {
            console.error('Erro ao obter dados do dashboard:', error);
        }
    });
}

if (typeof Chart !== 'undefined') {
    Chart.defaults.font.family = "'Segoe UI', Tahoma, Geneva, Verdana, sans-serif";
    Chart.defaults.font.size = 12;
    Chart.defaults.color = '#666';
    Chart.defaults.plugins.legend.display = true;
    Chart.defaults.plugins.tooltip.enabled = true;
    Chart.defaults.plugins.tooltip.backgroundColor = 'rgba(0, 0, 0, 0.8)';
    Chart.defaults.plugins.tooltip.padding = 12;
    Chart.defaults.plugins.tooltip.cornerRadius = 6;
}

function validarFormularioAlerta() {
    var tipoMoeda = $('#TipoMoeda').val();
    var tipoIndicador = $('#TipoIndicador').val();

    if (!tipoMoeda && !tipoIndicador) {
        alert('Selecione uma Moeda ou um Indicador');
        return false;
    }

    if (tipoMoeda && tipoIndicador) {
        alert('Selecione apenas Moeda OU Indicador, não ambos');
        return false;
    }

    return true;
}

function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}

function showLoader() {
    $('body').append('<div id="global-loader" class="loader"></div>');
}

function hideLoader() {
    $('#global-loader').remove();
}