

$(function () {
  $.connection.hub.url = "http://localhost:8080/signalr";
  var chatProxy = $.connection.messageHub;

  var data =
  {
    input: "Hej",
    responses: []
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
    data.responses.push(text);
  };
  $.connection.hub.start();
});
