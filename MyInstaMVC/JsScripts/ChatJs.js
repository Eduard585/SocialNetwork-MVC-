
var currentPage = 0;
var messageLeft = @Model.Chats.Count() - 10;
$(function () {

    var chat = $.connection.chatHub;
    chat.client.broadcastMessage = function (name, message) {
        var encodedImg = new Image();
        encodedImg.src = "@Url.Action("Avatar","User", new { Id = Model.currentChatUser })"
        var encodedMsg = $('<div />').text(message).html();
        var template = $('#message-template').html();
        $('#message-container').prepend(template);
        $("p:first").html(encodedMsg);
    };

    $('#displayname').val('@Model.currentChatUser');
    $('#message').focus();

    $.connection.hub.start().done(function () {
        $('#sendmessage').click(function () {
            if ($('#message').val() != "") {
                chat.server.send($('#displayname').val(), $('#message').val(),@Model.currentChatUser);
        $('#message').val('').focus();
    }
            });
        });

    });


function getMoreMessages() {
    currentPage += 1;
    messageLeft -= 10;
    if (messageLeft != 0) {
        $.get('@Url.Action("/GetChatMessages")', { currentPage: currentPage }).done(function (res) {
            $('#message-container').append(RenderMessages(res));
        }).always();
    }
}

function RenderMessages(data) {
    var temp = $('#message-template1').html();
    var template = Hogan.compile(temp);
    return template.render(data);
}

function init() {

}