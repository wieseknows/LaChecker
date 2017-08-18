$(document).ready(function () {

    $.ajax({
        url: "http://localhost:1833/api/topUsers",
        type: "get",
        dataType: "json",
        success: function (result) {
            loadTopUsersTable(result);
        },
        error: function () {
            alert("Error loading top users");
        }
    });

    $('#editor').bind('keypress', function (e) {
        if (e.which == 32) {
            $('#editor').each(function () {
                var words = $(this).text().toString().split(" ");

                var spans = "";
                $.each(words, function (key, value) {
                    spans += "<span>" + value.toString().trim() + "</span> ";
                });
                $(this).html(spans);
                placeCaretAtEnd(document.getElementById("editor"));
            });

            $('#editor span').hover(function () {
                var wordToIdentify = $(this).text().trim().toString();
                if (wordToIdentify.length < 4) {
                    return;
                }
                var dataToBeSend = {
                    word: wordToIdentify,
                    userId: $('#userId').val().toString() 
                }

                $.ajax({
                    url: "http://localhost:1833/api/identification",
                    type: "post",
                    data: JSON.stringify(dataToBeSend),
                    dataType: "json",
                    contentType: "application/json",
                    success: function (result) {
                        loadProbabilitiesTable(result);
                    },
                    error: function () {
                        alert("Error loading probabilities");
                    }
                });

                $('#tooltip').show();

                $.ajax({
                    url: "http://localhost:1833/api/topUsers",
                    type: "get",
                    dataType: "json",
                    success: function (result) {
                        loadTopUsersTable(result);
                    },
                    error: function () {
                        alert("Error loading top users");
                    }
                });
            }, function () {
                $('#tooltip').hide();
            });

            $('#editor span').mousemove(function (e) {
                $("#tooltip").css('top', e.pageY + 10).css('left', e.pageX + 20);
            });
        }
    });
});


function placeCaretAtEnd(el) {
    el.focus();
    if (typeof window.getSelection != "undefined"
            && typeof document.createRange != "undefined") {
        var range = document.createRange();
        range.selectNodeContents(el);
        range.collapse(false);
        var sel = window.getSelection();
        sel.removeAllRanges();
        sel.addRange(range);
    } else if (typeof document.body.createTextRange != "undefined") {
        var textRange = document.body.createTextRange();
        textRange.moveToElementText(el);
        textRange.collapse(false);
        textRange.select();
    }
}

function loadProbabilitiesTable(data) {
    var rows = '';
    rows += '<tr><th>Language</th>';
    rows += '<th>%</th></tr>';

    $.each(data, function (key, value) {
        if (key != "Id") {
            var row = '<tr>';
            row += '<td>' + key.toString() + '</td>';
            row += '<td>' + value.toString() + '</td>';
            rows += row + '</tr>';
        }
    });
    $('#probabilities').html(rows).fadeIn('slow');
}

function loadTopUsersTable(data) {
    var rows = '';
    rows += '<body><tr><th>#</th>';
    rows += '<th>Nickname</th>';
    rows += '<th>Score</th>';
    rows += '<th>Last Time Logged In</th>';
    rows += '<th>Average</th></tr>';
    $.each(data, function (key, topUser) {
        var row = '<tr>';
        row += '<td>' + (key + 1) + '</td>';
        row += '<td>' + topUser.Nickname + '</td>';
        row += '<td>' + topUser.TotalRequests + '</td>';
        row += '<td>' + topUser.LastLogIn + '</td>';
        row += '<td>' + topUser.AvgTimeBetweenRequests + '</td>';
        rows += row + '</tr>';
    });
    rows += "</body>";
    $('#topUsers').html(rows).hide();
    $('#topUsers').html(rows).fadeIn();
}