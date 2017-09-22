var fbUserData, providerValidation, loginTries;
//Facebook
//window.fbAsyncInit = function () {
//    FB.init({
//        appId: '2076889782535114',
//        status: true,
//        cookie: true,
//        xfbml: true
//    });
//};
$(document).ready(function () {
 
});
if (!fbUserData) {
    setLoginStatus(false);
}
function fbLogin() {
    if (fbUserData) { return;}
    FB.login(function (response) {
        if (response.status != "unknown") {
            FB.api('/me?fields=name,picture,email,first_name,last_name,link', function (response) {
                fbUserData = response;
                fbUserData["token"] = FB.getAccessToken();
                fbUserData["image"] = "http://graph.facebook.com/" + response.id + "/picture?type=normal";
                fbUserData["imageName"] = response.id + ".png";
                setLoginStatus(true);
            });
        }
        else {

        }
        // handle the response
    }, { scope: 'publish_actions,read_custom_friendlists,email' });
}

function setLoginStatus(state) {
    if (state) {
        $.post("/User/Login",
            {
                email: fbUserData.email,
                fullName: fbUserData.name,
                userId: fbUserData.id
            },
            function (data, status) {
                if (data.Role == "admin") {
                    $("#adminMenu").css("display", "inline-block");
                }
            });
        $("#loginBtn").hide();
        $("#user").show();
        $("#user img").attr("src", fbUserData.image);
        $("#user h4").text(fbUserData.name);
    }
    else {
        $("#loginBtn").show();
        $("#user").hide();
    }
}