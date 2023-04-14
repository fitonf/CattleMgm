// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var resources = null;

const ErrorNumber = {
    SUCCESS: 1,
    INFO: 2,
    WARNING: 3,
    ERROR: 4
}

const ReportType = {
    PDF: 1,
    EXCEL: 2,
    WORD: 3,
    POWERPOINT: 4,
    XML: 5,
    CSV: 6,
    JSON: 7,
    IMAGE: 8,
    HTML: 9
}

const SystemStatus = {
    DRAFT: 1,
    FINAL: 2,
    DELETED: 3,
    FINALISED: 4
}

$(document).ready(function () {
    resources = $.getJSON("/Internationalisation/General/" + culture + ".json", function () {
        errorTelMap = [resources.responseJSON["InvalidPhoneNumber"], resources.responseJSON["InvalidCountryCode"], resources.responseJSON["TooShort"], resources.responseJSON["TooLong"], resources.responseJSON["InvalidNumber"]];
    })

    $('table:not(.no-datatable)').DataTable({
        language: {
            url: "/Internationalisation/Datatables/" + culture + ".json"
        },
        keys: true,
        responsive: true,
        pageLength: $('table').attr('data-length')
    });
    //$('.select2').select2({ width: '100%' });
    if (Notification.permission === 'default') {
        $('#allow_notification').parent().removeClass('d-none');
    }
});

function DisplayNotification(message, title, icon, url, target, type) {

    var notiType = "";
    if (type == 1) {
        notiType = "bg-soft-success"
    } else if (type == 2) {
        notiType = "bg-soft-info"
    } else if (type == 3) {
        notiType = "bg-soft-warning"
    } else if (type == 4) {
        notiType = "bg-soft-pink"
    }

    $("#notification-title").text(title);
    $("#notification-message").text(message);
    $("#notification-icon").addClass(icon);
    $("#notification-bg").addClass(notiType);

    $("#notification-modal").modal('show');
}

function ChangeMode(e) {
    $.post('/Home/ChangeMode', { mode: $(e).is(':checked') }, function (data) {
        if (data.ErrorNumber == ErrorNumber.SUCCESS) {
            window.location.reload();
        }
    })
}

function ChangeModeAdmin(e) {
    $.post('/Home/ChangeModeAdmin', { mode: $(e).is(':checked') }, function (data) {
        if (data.ErrorNumber == ErrorNumber.SUCCESS) {
            window.location.reload();
        }
    })
}

function ShowLoading() {
    $('#loader').modal('show')
}

function HideLoading(timeout = 0) {
    setTimeout(function () { $('#loader').modal('hide') }, timeout)
}

$(document).on('submit', 'form:not(.noLoading)', function (t) {
    ShowLoading()
    $(this).find('button[type="submit"]').attr('disabled', 'disabled')
});

$(document).ajaxComplete(function (event, xhr, settings) {
    if (settings.port != "validatePersonalNumber") {
        HideLoading(500);
    }

    $(this).find('button[type="submit"]').removeAttr('disabled', 'disabled')
})

$(document).ajaxError(function (error) {
    debugger;
    if (error.handleObj.handler.arguments[1].status == 403) {
        Swal.fire({
            icon: 'error',
            title: resources.responseJSON["AccessDenied"],
            text: resources.responseJSON["AccessDeniedText"],
            confirmButtonText: resources.responseJSON["Okay"]
        })
    } else if (error.handleObj.handler.arguments[1].status == 0) {
        //It is not an error. It's just abortion of an ajax call from another ajax call.
    }
    else {
        Swal.fire({
            icon: 'error',
            title: "Gabim",
            text: "Ka ndodhur një gabim",
            confirmButtonText: "Në rregull"
        })
    }
})

$(document).on('invalid-form.validate', 'form', function () {
    HideLoading();
}); 

$('#notification-link-icon').click(function () {
    GetNotifications();
})

function OpenNotification(id) {
    $('#modal-default-top').find('.modal-content').load('/Home/_OpenNotification/' + id, function () {
        $('#modal-default-top').modal('toggle')
    })
}

tippy('.tippy-btn');
tippy('#myElement', {
    html: document.querySelector('#feature__html'), // DIRECT ELEMENT option
    arrow: true,
    animation: 'fade'
});