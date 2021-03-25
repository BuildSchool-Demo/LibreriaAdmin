Date.prototype.yyyymmdd = function () {
    var mm = this.getMonth() + 1;
    var dd = this.getDate();
    return [this.getFullYear(),
        '/',
    (mm > 9 ? '' : '0') + mm,
        '/',
    (dd > 9 ? '' : '0') + dd
    ].join('');
};




var form = new Vue({
    el: '#email',
    data: {
        AddVerify: true,
        inputData: {
            ExhibitionId: exhibitionData.EmailList[0].ExhibitionId,
            ExCustomerId: exhibitionData.EmailList[0].ExCustomerId,
            ExCustomerName: exhibitionData.EmailList[0].ExCustomerName,
            ExCustomerPhone: exhibitionData.EmailList[0].ExCustomerPhone,
            ExCustomerEmail: exhibitionData.EmailList[0].ExCustomerEmail,
            ExhibitionPrice: exhibitionData.EmailList[0].ExhibitionPrice,
            ExhibitionStartTime: exhibitionData.EmailList[0].ExhibitionStartTime,
            ExhibitionEndTime: exhibitionData.EmailList[0].ExhibitionEndTime,
            ExName: exhibitionData.EmailList[0].ExName,
            MasterUnit: exhibitionData.EmailList[0].MasterUnit,
            ExPhoto: exhibitionData.EmailList[0].ExPhoto,
            ExhibitionIntro: exhibitionData.EmailList[0].ExhibitionIntro,
            CustomerVerify: exhibitionData.EmailList[0].CustomerVerify,
            ReviewState: exhibitionData.EmailList[0].ReviewState,
            isValid: true,
            changeimg: true,
            img:'https://imgur.com/6qcWtCf',
        },
        inputDataCheck: {
            AccountError: false,
            AccountErrorMsg: '',
            PasswordError: false,
            PasswordErrorMsg: '',
            CheckPasswordError: false,
            CheckPasswordErrorMsg: '',
        },
        startDateOptions: [
            { value: exhibitionData.EmailList[0].ExhibitionStartTime, text: `${new Date(exhibitionData.EmailList[0].ExhibitionStartTime).getFullYear()}年${new Date(exhibitionData.EmailList[0].ExhibitionStartTime).getMonth() + 1}月${new Date(exhibitionData.EmailList[0].ExhibitionStartTime).getDate()}日` },
        ],
        endDateOptions: [
            { value: exhibitionData.EmailList[0].ExhibitionEndTime, text: `${new Date(exhibitionData.EmailList[0].ExhibitionEndTime).getFullYear()}年${new Date(exhibitionData.EmailList[0].ExhibitionEndTime).getMonth() + 1}月${new Date(exhibitionData.EmailList[0].ExhibitionEndTime).getDate()}日` }
        ]

    },
    //created: function () {
    //    axios.get("/api/Exhibiton/EmailGetAll/")
    //        .then((res) => {
    //            console.log(res);
    //            this.inputData = res.data.body.emailList;
    //            this.isBusy = false;
    //        })
    //},
    //watch: {
    //    'inputData.Account': {
    //        immediate: true,
    //        handler: function () {
    //            if (this.inputData.Account == '') {
    //                this.inputDataCheck.AccountError = true;
    //                this.inputDataCheck.AccountErrorMsg = '帳號不得為空';
    //            }
    //            else if (this.inputData.Account.length < 8) {
    //                this.inputDataCheck.AccountError = true;
    //                this.inputDataCheck.AccountErrorMsg = '帳號長度不可小於八碼';
    //            }
    //            else {
    //                this.inputDataCheck.AccountError = false;
    //                this.inputDataCheck.AccountErrorMsg = '';
    //            }
    //        }
    //    },
    //    'inputData.Password': function () {
    //        let passwordRegexp = /^[0-9]*$/;
    //        this.inputDataCheck.CheckPasswordError = false;
    //        this.inputDataCheck.CheckPasswordErrorMsg = '';
    //        if (!passwordRegexp.test(this.inputData.Password)) {
    //            this.inputDataCheck.PasswordError = true;
    //            this.inputDataCheck.PasswordErrorMsg = '密碼必須為數字';
    //        }
    //        else if (this.inputData.Password == '') {
    //            this.inputDataCheck.PasswordError = true;
    //            this.inputDataCheck.PasswordErrorMsg = '密碼不得為空';
    //        } else if (this.inputData.Password.length < 8) {
    //            this.inputDataCheck.PasswordError = true;
    //            this.inputDataCheck.PasswordErrorMsg = '密碼長度不可小於八碼';
    //        }
    //        else {
    //            this.inputDataCheck.PasswordError = false;
    //            this.inputDataCheck.PasswordErrorMsg = '';
    //        }
    //    },
    //    'inputData.CheckPassword': function () {
    //        if (this.inputData.CheckPassword != this.inputData.Password) {
    //            this.inputDataCheck.CheckPasswordError = true;
    //            this.inputDataCheck.CheckPasswordErrorMsg = '密碼必須相同';
    //        }
    //        else {
    //            this.inputDataCheck.CheckPasswordError = false;
    //            this.inputDataCheck.CheckPasswordErrorMsg = '';
    //        }
    //    }
    //},
    methods: {
        modify(event) {
            this.$data.inputData.isValid = !this.$data.inputData.isValid
            let startDate = new Date(exhibitionData.EmailList[0].RentalDate.StartDate)
            let endDate = new Date(exhibitionData.EmailList[0].RentalDate.EndDate)

            Date.prototype.addDays = function (days) {
                let dat = new Date(this.valueOf())
                dat.setDate(dat.getDate() + days);
                return dat;
            }

            function getDates(startDate, endDate) {
                let dateArray = new Array();
                let currentDate = startDate;
                while (currentDate <= endDate) {
                    dateArray.push(currentDate)
                    currentDate = currentDate.addDays(1);
                }
                return dateArray;
            }

            let dateArray = getDates(startDate, endDate);

            let optionsArray = new Array();
            for (i = 0; i < dateArray.length; i++) {
                let value = dateArray[i].yyyymmdd();
                let text = `${dateArray[i].getFullYear()}年${dateArray[i].getMonth()+1}月${dateArray[i].getDate()}日`
                let options = { value: value, text: text };
                optionsArray.push(options);
            }
            
            this.startDateOptions = optionsArray;
            this.endDateOptions = '';

            let start = document.getElementById('startDate');
            start.addEventListener('change', (event) => {
                let endDateArray = new Array();
                for (i = 0; i < optionsArray.length; i++) {
                    if (event.currentTarget.value < optionsArray[i].value) {
                        endDateArray.push(optionsArray[i])
                    }
                }
                this.endDateOptions = endDateArray;
            })
        },
        modifySubmit: function () {
            this.$data.inputData.isValid = !this.$data.inputData.isValid

            //客戶回覆狀態變更
            let customerVerify = this.$data.inputData.CustomerVerify;
            if (customerVerify == false) {
                customerVerify = this.$data.inputData.CustomerVerify = !this.$data.inputData.CustomerVerify
            }


            let result = {
                ExhibitionId: this.$data.inputData.ExhibitionId,
                ExCustomerId: this.$data.inputData.ExCustomerId,
                ExCustomerName: document.getElementById("exCustomerName").value,
                ExCustomerPhone: document.getElementById("exCustomerPhone").value,
                ExCustomerEmail: document.getElementById("exCustomerEmail").value,
                ExhibitionPrice: document.getElementById("exhibitionPrice").value,
                ExhibitionStartTime: document.getElementById("startDate").value,
                ExhibitionEndTime: document.getElementById("endDate").value,
                ExName: document.getElementById("exName").value,
                MasterUnit: document.getElementById("masterUnit").value,
                ExPhoto: this.$data.inputData.ExPhoto,
                ExhibitionIntro: document.getElementById("textarea-rows").value,
                CustomerVerify: customerVerify,
                ReviewState: this.$data.inputData.ReviewState,
            }

            axios({
                method: 'POST',
                headers: { 'content-type': 'application/json' },
                data: JSON.stringify(result),
                url: '/api/Exhibiton/ModifyExhibition',
            })
                .then((res) => {
                    if (res.data.isSuccess == true) {
                        alert("已修改，後續會再與您做最後確認展覽資訊，謝謝!")
                    }
                })
                .catch((err) => {
                    console.log(err.data)
                })
        },
        Submit: function () {
            let customerVerify = this.$data.inputData.CustomerVerify;
            let reviewState = this.$data.inputData.ReviewState
            if (customerVerify == false) {
                customerVerify = this.$data.inputData.CustomerVerify =!this.$data.inputData.CustomerVerify
            }
            if (reviewState == false) {
                reviewState = this.$data.inputData.ReviewState =!this.$data.inputData.ReviewState
            }

            let result = {
                ExhibitionId: this.$data.inputData.ExhibitionId,
                ExCustomerId: this.$data.inputData.ExCustomerId,
                CustomerVerify: customerVerify,
                ReviewState: reviewState
            }
            axios({
                method: 'POST',
                headers: { 'content-type': 'application/json' },
                data: JSON.stringify(result),
                url: '/api/Exhibiton/ConfirmEmail',
            })
            .then((res) => {
                if (res.data.isSuccess == true) {
                    alert("已確認，展覽資訊會馬上上傳至展演區，")
                }
            })
            .catch((err) => {
                console.log(err.data)
            })
        },
        cancel: function () {
            this.$data.inputData.isValid = !this.$data.inputData.isValid

        },
        photo: function () {
            let file = $('#exPhoto__BV_file_outer_ input')[0].files[0];
            let reader = new FileReader;
            reader.onload = function (e) {
                //this.inputData.changeimg = !this.inputData.changeimg
                ExPhoto = file
                let i = URL.createObjectURL(file)
                //$('#upload-image label').css('background-image', 'url("' + e.target.result + '")');

            };
            reader.readAsDataURL(file);
        }
    }
});