$(function () {
  $.connection.hub.url = ServerBaseUrl;
  var chatProxy = $.connection.messageHub;

  var data =
  {
    input: "Hej",
    chatEntries: [],
    connectionInfoVisible: true,
    connectionMessage: "Connecting ...",
    reconnectVisible: false
  };

  var app = new Vue(
  {
    el: "#chat",
    data: data,
    methods: {
      say: function (event) {
        if (this.input) {
          AddMyChatEntry(data, this.input);
          chatProxy.server.say(ServerToken, this.input)
                          .fail(function(e) { window.alert(e.message) });
          this.input = "";
        }
        SetFocusInInput();
      },
      reconnect: function (event) {
        NotifyConnection(data, "Reconnecting", true);
        $.connection.hub.start();
        NotifyConnection(data, "", false);
      }
    }
  });

  chatProxy.client.newMessage = function (args) {
    AddChatEntry(data, args.Name, args.Timestamp, args.Message, 'left');
  };
  $.connection.hub.reconnecting(function () { NotifyConnection(data, "Reconnecting", false); });
  $.connection.hub.reconnected(function () { NotifyConnection(data, null, false); });
  $.connection.hub.disconnected(function () { NotifyConnection(data, "Disconnected", true); });
  $.connection.hub.start();
  SetFocusInInput();

  // Connected and running. Hide connection message.
  NotifyConnection(data, "", false)
});


function AddMyChatEntry(data, input)
{
  AddChatEntry(data, UserName, new Date(), input, "right");
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

function NotifyConnection(data, msg, reconnectVisible) {
  data.connectionMessage = msg;
  data.reconnectVisible = reconnectVisible;
}
