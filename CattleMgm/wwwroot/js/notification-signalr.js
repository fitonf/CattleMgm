
const NotiType = {
    Success: 1,
    Info: 2,
    Warning: 3,
    Error: 4
}

Object.defineProperty(WebSocket, 'OPEN', { value: 1, });

connection.on("Notification", function (notification) {
    GetNotifications()
    DisplayNotification(notification.message, notification.title, notification.icon, notification.url, notification.target, notification.type);
});

connection.on('UnReadNotification', function (result) {
    if (result > 0) {
        $('.notification-number').text(result).show();
    } else {
        $('.notification-number').hide();
    }
})

connection.on("LastTenNotification", function (notifications) {
    $('#notification-box-test').empty();

    var bgColor = 'bg-soft-success';

    $.each(notifications, function (i, v) {
        if (v.notificationType == NotiType.Success) {
            bgColor = "bg-soft-success"
        } else if (v.notificationType == NotiType.Warning) {
            bgColor = "bg-soft-warning"
        } else if (v.notificationType == NotiType.Info) {
            bgColor = "bg-soft-info"
        } else if (v.notificationType == NotiType.Error) {
            bgColor = "bg-soft-pink"
        }

        var newNoti = $('<a></a>', {
            href: v.url != null ? v.url : "javascript:;",
            onclick: v.url != null ? '' : 'OpenNotification(' + v.id + ')',
            class: 'dropdown-item notification-item py-3',
            html: $('<small></small>', {
                class: 'float-end text-muted ps-2',
                text: v.daysAgo
            })
        }).append($('<div></div>', {
            class: 'media',
        }).append($('<div></div>', {
            class: 'avatar-md ' + bgColor,
            html: $('<i></i>', {
                class: 'align-self-center icon-xs ' + v.icon
            })
        })).append($('<div></div>', {
            class: 'media-body align-self-center ms-2 text-truncate',
            html: $('<h6></h6>', {
                class: 'my-0 fw-normal text-dark',
                text: v.title
            })
        }).append($('<small></small>', {
            class: 'text-muted mb-0',
            text: v.description
        })))
        )

        $('#notification-box-test').append(newNoti);
    })
});

function GetNotifications(rowNumber) {
    if (rowNumber != null) {
        var x = String(rowNumber);
    };
    connection.invoke('GetNotifications', x)
}

function UnreadNotifications() {
    connection.invoke('GetUnreadNotifications')
}

connection.start().then(function () {
    GetNotifications();
})