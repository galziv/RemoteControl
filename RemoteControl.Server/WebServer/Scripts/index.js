

var lastPoint = { x: null, y: null };
var touchInterval;
var volumeIntervalMs = 70;
var keyboardInput;

function onTouchEnd() {
    lastPoint = { x: null, y: null };
}

function sendTouchDelta(e) {

    e.stopPropagation();

    var xDelta = e.changedTouches[0].pageX - lastPoint.x;
    var yDelta = e.changedTouches[0].pageY - lastPoint.y;

    if (!lastPoint.x) {
        sendDelta(0, 0, e.changedTouches[0].pageX, e.changedTouches[0].pageY);
    } else {
        sendDelta(xDelta, yDelta, e.changedTouches[0].pageX, e.changedTouches[0].pageY);
    }

    document.body.scrollLeft = 0;
    document.body.scrollTop = 0;
}

function sendMouseDelta(e) {

    var xDelta = e.clientX - lastPoint.x;
    var yDelta = e.clientY - lastPoint.y;

    if (!lastPoint.x) {
        sendDelta(0, 0, e.clientX, e.clientY);
    } else {
        sendDelta(xDelta, yDelta, e.clientX, e.clientY);
    }
}

function sendDelta(xDelta, yDelta, lastX, lastY) {
    var div = document.querySelector('#mouse-coordinates');
    div.innerHTML = 'x: ' + xDelta + ', y: ' + yDelta;
    div.innerHTML += '<br/>';
    div.innerHTML += 'xDelta: ' + xDelta + ', yDelta: ' + yDelta;

    lastPoint.x = lastX;
    lastPoint.y = lastY;

    if (Math.abs(xDelta) < 2 && Math.abs(yDelta) < 2) {
        return;
    }

    $.ajax({
        method: "GET",
        url: "api/Control/MoveMouseBy?xDelta=" + xDelta.toFixed(0) * 2 + "&yDelta=" + yDelta.toFixed(0) * 2
    });
}

function sendLeftClick(e) {

    e.stopPropagation();

    $.ajax({
        method: "GET",
        url: "api/Control/MouseLeftClick"
    });
}

function sendRightClick(e) {

    e.stopPropagation();

    $.ajax({
        method: "GET",
        url: "api/Control/MouseRightClick"
    });
}

function sendSpecialKey(e, key) {

    e.stopPropagation();

    $.ajax({
        method: "GET",
        url: "api/Control/" + key
    });
}

function sendSpecialKeyContinues(e, key) {

    e.stopPropagation();

    $.ajax({
        method: "GET",
        url: "api/Control/" + key
    });

    touchInterval = setInterval(function () {
        console.log(1);
        $.ajax({
            method: "GET",
            url: "api/Control/" + key
        });
    }, volumeIntervalMs);
}

function sendMute(e) {
    e.stopPropagation();

    $.ajax({
        method: "GET",
        url: "api/Control/KeyboardMute" + key
    });
}

function keyboardClick(e) {
    e.stopPropagation();


    if (isKeyboardOpen) {
        document.querySelector('#mouse-pad').focus();
    } else {
        keyboardInput.focus();
    }

    isKeyboardOpen = !isKeyboardOpen;
}

var isKeyboardOpen = false;

function pressReturn(e) {

    e.stopPropagation();

    $.ajax({
        method: "GET",
        url: "api/Control/KeyboardKey?key=RETURN"
    });
}
function pressMediaKey(e, mediaKey) {

    e.stopPropagation();

    $.ajax({
        method: "GET",
        url: "api/Control/MediaKey?key=" + mediaKey
    });
}

function onKeyup(e) {

    try {

        var letter = keyboardInput.value;

        switch (letter) {
            case "\n":
                letter = "RETURN";
                break;
            case "":
                letter = "BACK";
                break;
            case " ":
                letter = "SPACE";
                break;
        }

        keyboardInput.value = '';

        e.stopPropagation();

        $.ajax({
            method: "GET",
            url: "api/Control/KeyboardKey?key=" + letter
        });
    } catch (e) {
        alert(e);
    }
}

$(function () {
    keyboardInput = document.querySelector('#keyboard-input');
});


function getPage(page) {

    $.ajax({
        method: "GET",
        url: "api/Control/GetPage?url=" + page
    }).success(function (data) {
        var $response = $(data);
        $("head").replaceWith($response.find("head"));
        $("body").replaceWith($response.find("body"));
    });
}