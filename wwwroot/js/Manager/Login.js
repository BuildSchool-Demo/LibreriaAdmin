let jwtAuthUrl = "https://localhost:5001/api/Manager/Login";

function axios() {
    $.axios({
        url: jwtAuthUrl,
        method: "POST",
        dataType: "json",
        data: JSON.stringify(request),
        contentType: "application/json;charset=UTF-8",  
        success: function (response) {
            localStorage.setItem("jwtToken", response.token);

            $("#token").html("JWT Token: " + response.token);
        }
    });
}
