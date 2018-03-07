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
        AddChatEntry(data, "Mig", "Nu", this.input, "right");
        chatProxy.server.say(this.input);
        this.input = "";
      }
    }
  });

  chatProxy.client.newMessage = function (text) {
    AddChatEntry(data, "Noah", "Today", text, 'left');
  };
  $.connection.hub.start();
});


function AddChatEntry(data, name, date, text, position) {
  var entry =
    {
      name: name,
      date: date,
      text: text,
      position: position
    };
  data.chatEntries.push(entry);
}
