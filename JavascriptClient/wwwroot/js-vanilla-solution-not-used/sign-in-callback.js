var extractTokens = function (address) {

    var returnValue = address.split("#")[1];
    var values = returnValue.split("&");

    for(var i = 0; i < values.length; i++) {
        var currentValue = values[i];
        var keyValuePair = currentValue.split("=");
        localStorage.setItem("app_" + keyValuePair[0], keyValuePair[1]); // app prefix so this doesnt get overwritten by similar items from external providers
    }

    window.location.href = "/home/index";
}

extractTokens(window.location.href);