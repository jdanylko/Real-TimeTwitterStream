$(function () {

    var twitterHub = $.connection.twitterHub;

    twitterHub.client.setTaskId = function (id) {
        $("#firehoseOff").attr("data-id", id);
    }

    twitterHub.client.updateStatus = function (status) {
        $("#firehoseStatus").html(status);
    }

    twitterHub.client.updateTweet = function (tweet) {
        $(tweet.HTML)
            .hide()
            .prependTo(".tweets")
            .fadeIn("slow");
    };

    $("#firehoseOn").on("click", function () {
        twitterHub.server.startTwitterLive();
    });

    $("#firehoseOff").on("click", function () {
        var id = $(this).attr("data-id");
        twitterHub.server.stopTwitterLive(id);
    });

    $.connection.hub.start();

});
