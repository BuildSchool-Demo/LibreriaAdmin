
//用Form之外的Button Click事件觸發執行Create和Delete
let backendApi = "https://localhost:5001/api/Manager";

window.onload = function () {

    //Create
    let btnCreate = document.getElementById("btnCreate");
    btnCreate.addEventListener("click", function () {
        //抓input 的 value
        let managerUserName  = document.getElementById("managerUserName").value;
        let managername = document.getElementById("managerName").value;
        let managerPassword = document.getElementById("managerPassword").value;
        let managerRoleID = document.getElementById("managerRoleID").value;

        let product = { ManagerUserName: managerUserName, ManagerName: managername, ManagerPassword: managerPassword, ManagerRoleID: managerRoleID }


        $.ajax({
            url: backendApi + "/" + "CreateManager",
            method: "POST",
            dataType: "json",
            contentType: "application/json;charset=UTF-8",
            data: JSON.stringify(product),
            success: function (data, textStatus, jqXHR) {
                document.getElementById("managerUserName").value = data.ManagerUserName;
                document.getElementById("managerName").value = data.ManagerName;
                document.getElementById("managerPassword").value = data.ManagerPassword;
                document.getElementById("managerRoleID").value = data.managerRoleID;

                result.innerText = `Status : ${textStatus}資料新增成功, location : ${jqXHR.getResponseHeader('location')}`;


            },
            error: function (jqXHR, textStatus, errorThrown) {
                result.innerText = textStatus + "," + jqXHR.state;

            }

        });
    });
}