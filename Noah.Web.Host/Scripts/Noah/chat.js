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
        AddMyChatEntry(data, this.input);
        chatProxy.server.say(this.input);
        this.input = "";
      }
    }
  });

  chatProxy.client.newMessage = function (args) {
    AddChatEntry(data, args.Name, args.Timestamp, args.Message, 'left');
  };
  $.connection.hub.start();
});


function AddMyChatEntry(data, input)
{
  AddChatEntry(data, "Jørn", "Nu", input, "right");
}


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
