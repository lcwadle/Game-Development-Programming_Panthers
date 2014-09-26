$(document).ready(function () {
    var con = $.connection.updateHub;

    con.client.callClient = function (msg) {
        $('#showMsgs').append('<li>' + msg + '</li>');
        $('#txtBox').val('');
    };

    $.connection.hub.start();

    $('#txtBox').keyup(function (event) {
        if (event.keyCode == 13) {
            con.server.callServer($(this).val());
        }
    });
});