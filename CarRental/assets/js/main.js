﻿var fbUserData, providerValidation, loginTries;
var rentedCars;
var baseCurrency = "USD";
var currencyList = [];
$(document).ready(function () {
    if ($("#rentBtn").length > 0) {
        rentedCars = sessionStorage.getItem("rentedCars");
        if (rentedCars && rentedCars.length > 0) {
            var carId = $("#carId").val();
            for (var i = 0; i < rentedCars.length; i++) {
                if (rentedCars[i] == carId) {
                    $("#rentBtn").hide();
                    $("#rentMessage").show();
                    break;
                }
            }
        }
    }
    if ($(".manufactures").length > 0) {
        manageContext(1);
    }
    if ($(".search-results").length > 0) {
        getCurrency();
    }
    if ($(".price").length > 0) {
        $("#currPrice").show();
    }
});
if (!fbUserData) {
    setLoginStatus(false);
}
function fbLogin() {
    if (fbUserData) { return; }
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

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

function rent(carId) {
    if (!fbUserData) {
        alert("You must login in order to rent a car");
    }
    else {
        $.ajax({
            type: "GET",
            url: "/User/GetLoggedUser",
            data: {
                userId: fbUserData.id
            },
            dataType: "text",
            success: function (data) {
                var user = JSON.parse(data);
                $.ajax({
                    type: "POST",
                    url: "/User/Rent",
                    data: {
                        pickupDate: getParameterByName("pickupDate"),
                        returnDate: getParameterByName("returnDate"),
                        userId: user.ID,
                        carId: carId
                    },
                    dataType: "text",
                    success: function (data) {
                        if (data == "success") {
                            $("#rentBtn").hide();
                            $("#rentMessage").show();
                            $("#rentModal").modal();
                            rentedCars = sessionStorage.getItem("rentedCars");
                            if (!rentedCars) {
                                rentedCars = [];
                                rentedCars.push($("#carId").val());
                            }
                            else {
                                rentedCars.push($("#carId").val());
                            }
                            sessionStorage.setItem("rentedCars",JSON.parse(rentedCars));
                        }
                    }
                });
            }
        });

    }
}

var c = document.getElementById("myCanvas");
if (c) {
    var ctx1 = c.getContext("2d");
    ctx1.moveTo(0, 0);
    ctx1.lineTo(200, 100);
    ctx1.stroke();
}
function manageContext(id) {
    switch (id) {
        case 1:
            $(".manufactures").show();
            $(".models").hide();
            $(".orders").hide();
            break;
        case 2:
            $(".manufactures").hide();
            $(".models").show();
            $(".orders").hide();
            break;
        case 3:
            $(".manufactures").hide();
            $(".models").hide();
            $(".orders").show();
            break;
    }
}
//Calculate the price based on the requested currency and perform a GET request to receive the list of new currencies
function getCurrency(currency) {

    if (currency) {
        baseCurrency = currency;
        $("#currPrice").text(currency);
        $(".price").each(function (i, obj) {
            var oldPricePerDay = parseInt(obj.innerText) * currencyList[currency];
            oldPricePerDay = Math.round(oldPricePerDay).toFixed(2)
            obj.innerText = oldPricePerDay;

        });
        $(".price-wrap small").text(currency);
        
    }
    $.ajax({
        type: "GET",
        url: "http://api.fixer.io/latest?base=" + baseCurrency,
        dataType: "text",
        success: function (data) {
            //Get the updated currency list
            currencyList = JSON.parse(data).rates;
            $("#currencyItems").empty();
            for (var property in currencyList) {
                if (currencyList.hasOwnProperty(property)) {
                    var currencyOption = "<li><a href='#' onclick=getCurrency('" + property + "')>" + property + "</a></li>";
                    $("#currencyItems").append(currencyOption);
                }
            }
        }
    });
}

function shareCarOnFacebook() {
    FB.login(function () {
        // Note: The call will only work if you accept the permission request
        FB.api('/me/feed', 'post', { message: 'Check out wheel car agency!! ' });
    }, { scope: 'publish_actions' });
}