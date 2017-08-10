var renderDateTime = function() {
    var spans = document.getElementsByTagName("span");

    for (var i = 0; i < spans.length; i++) {
        var span = spans[i];
        var utcTimeString = span.getAttribute("data-utc-time");

        if (utcTimeString != null) {
            var utcDateTime = new Date(utcTimeString);
            var offsetMilliseconds = new Date().getTimezoneOffset() * 60 * 1000;
            var localDateTime = new Date(utcDateTime.valueOf() + offsetMilliseconds);
            var localDate = localDateTime.toLocaleDateString();
            var localTime = localDateTime.toLocaleTimeString();
            var textNode = document.createTextNode(localDate + " " + localTime);

            span.innerText = textNode.textContent;
        }
    }
}

renderDateTime();
