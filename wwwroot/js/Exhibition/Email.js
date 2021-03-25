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
            isValid: true
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
    //    axios.get("/api/Exhibiton/GetEmailData")
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
        checkAddVerify() {
            for (let index in this.inputDataCheck) {
                if (this.inputDataCheck[index] == true) {
                    thid.AddVerify = false;
                    return;
                }
            }
            this.AddVerify = true;
        },
        modify(event) {
            this.$data.inputData.isValid = !this.$data.inputData.isValid
            let startDate = new Date(getRentalDate.GetRentalDate[0].StartDate)
            let endDate = new Date(getRentalDate.GetRentalDate[0].EndDate)

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
            this.$data.inputData.isValid =! this.$data.inputData.isValid
        }
    }
});