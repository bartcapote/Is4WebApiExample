
// use some smart algorithms here to generate those (next two functions)
var createState = function () {
    return "SessionValueMakeItLongerJohnnysfkjashfkjdhsfkjhdsjhfdkjshfdkjshfkjdshfkjdsfds"
}

var createNonce = function () {
    return "NonceValuesdasdasdasdasdasdasdasfdskhfgdskjhfdkajugfdkjsgf"
}


var signIn = function () {
    var redirectUri = "https://localhost:44366/Home/SignIn"; // TODO store this somewhere safe?
    var responseType = "id_token token";
    var scope = "openid MyApiOne";

    var authUrl =
        "/connect/authorize/callback" +
        "?client_id=client_id_js" +
        "&redirect_uri=" + encodeURIComponent(redirectUri) +
        "&response_type=" + encodeURIComponent(responseType) +
        "&scope=" + encodeURIComponent(scope) +
        "&nonce=" + createNonce() +
        "&state=" + createState();

    var returnUrl = encodeURIComponent(authUrl);

    
    console.log(authUrl);
    console.log(returnUrl);

    window.location.href = "https://localhost:44324/Auth/Login?ReturnUrl=" + returnUrl;
}
