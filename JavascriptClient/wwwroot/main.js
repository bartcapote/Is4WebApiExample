var config = {
    userStore: new Oidc.WebStorageStateStore({ store: window.localStorage }), // use local storage instead of session storage
    authority: "https://localhost:44324/",
    client_id: "client_id_js",
    response_type: "id_token token",
    redirect_uri: "https://localhost:44366/Home/SignIn",
    scope: "openid MyApiOne Blob"
};

var userManager = new Oidc.UserManager(config);

var signIn = function () {
    userManager.signinRedirect();
};

userManager.getUser().then(user => {
    console.log("user:", user)
    if(user) {
        axios.defaults.headers.common["Authorization"] = "Bearer " + user.access_token;
    }
});

var callApi = function () {
    axios.get("https://localhost:44358/secret")
    .then(result => {
        console.log(result);
    })
}