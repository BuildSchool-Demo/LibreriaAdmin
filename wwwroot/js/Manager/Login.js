
$("#submitLogin").click(function () {
    let username = document.getElementById("username").value;
    let password = document.getElementById("password").value;

    loginVM = { "username": username, "password": password };


    $.ajax({
        url: "/api/Manager/Login",
        method: "POST", 
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        data: JSON.stringify(loginVM),
        headers: {
            //"content-type":"application/json; charset=utf-8"
        },
        success: function (response) {

            localStorage.setItem("jwtToken", response.token);
            Cookies.set("jwtToken", response.token);

            $("#token").html("JWT Token: " + response.token);
            AfterLogin();
            
        }
    });

    function AfterLogin() {
        window.location.href = '/Home/Index';
    }
});