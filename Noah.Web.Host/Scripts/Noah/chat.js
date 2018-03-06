$(function () {
  $.connection.hub.url = ServerBaseUrl;
  var chatProxy = $.connection.messageHub;

  var data =
  {
    input: "Hej",
    chatEntries: []
  };

  var app = new Vue(
  {
    el: "#chat",
    data: data,
    methods: {
      say: function (event) {
        chatProxy.server.say(this.input);
      }
    }
  });

  chatProxy.client.newMessage = function (text) {
    AddChatEntry(data, text, 'left');
  };
  $.connection.hub.start();
});


function AddChatEntry(data, text, position) {
  var entry =
    {
      text: text,
      position: position
    };
  data.chatEntries.push(entry);
}
