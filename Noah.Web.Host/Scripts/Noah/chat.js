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
        if (this.input) {
          AddMyChatEntry(data, this.input);
          chatProxy.server.say(this.input);
          this.input = "";
        }
        SetFocusInInput();
      }
    }
  });

  chatProxy.client.newMessage = function (args) {
    AddChatEntry(data, args.Name, args.Timestamp, args.Message, 'left');
  };
  $.connection.hub.start();
  SetFocusInInput();
});


function AddMyChatEntry(data, input)
{
  AddChatEntry(data, "Jørn", new Date(), input, "right");
}


function AddChatEntry(data, name, date, text, position) {
  var entry =
    {
      name: name,
      date: moment(date),
      text: text,
      position: position
    };
  data.chatEntries.push(entry);
}

function SetFocusInInput() {
  $("#chatInput").focus();
}