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
        isError: false,
        isBusy: true,
        successShow: false,
        dangerShow: false,
        AddVerify: true,
        disabled: true,
        btnDisabled:false,
        inputData: {
            isValid: true,
        },
        inputDataCheck: {
            NameError: false,
            NameErrorMsg: '',
            PhoneError: false,
            PhoneErrorMsg: '',
            EmailError: false,
            EmailErrorMsg: '',
            PriceError: false,
            PriceErrorMsg: '',
            StartTimeError: false,
            StartTimeErrorMsg: '',
            EndTimeError: false,
            EndTimeErrorMsg: '',
            ExNameError: false,
            ExNameErrorMsg: '',
            MasterUnitError: false,
            MasterUnitErrorMsg: '',
            PhotoError: false,
            PhotoErrorMsg: '',
            IntroError: false,
            IntroErrorMsg: '',
        },
        startDateOptions: [],
        endDateOptions: [],
        successShowMsg:'',

    },
    created: function () {

        axios.get("/api/Exhibiton/EmailGetAll/" + exhibitionId)
            .then((res) => {
                let item = res.data.body.emailList[0];
                form.$set(form.$data.startDateOptions, 0, {
                    value: item.exhibitionStartTime,
                    text: `${new Date(item.exhibitionStartTime).getFullYear()}年${new Date(item.exhibitionStartTime).getMonth() + 1}月${new Date(item.exhibitionStartTime).getDate()}日`
                })

                form.$set(form.$data.endDateOptions, 0, {
                    value: item.exhibitionEndTime,
                    text: `${new Date(item.exhibitionEndTime).getFullYear()}年${new Date(item.exhibitionEndTime).getMonth() + 1}月${new Date(item.exhibitionEndTime).getDate()}日`
                })

                form.$data.inputData = Object.assign({}, form.$data.inputData, {
                    ExhibitionId: item.exhibitionId,
                    ExCustomerId: item.exCustomerId,
                    ExhibitionStartTime: item.exhibitionStartTime,
                    ExhibitionEndTime: item.exhibitionEndTime,
                    ExhibitionIntro: item.exhibitionIntro,
                    MasterUnit: item.masterUnit,
                    ExhibitionPrice: item.exhibitionPrice,
                    ExPhoto: item.exPhoto,
                    ExName: item.exName,
                    ExCustomerName: item.exCustomerName,
                    ExCustomerPhone: item.exCustomerPhone,
                    ExCustomerEmail: item.exCustomerEmail,
                    RentalDate: item.rentalDate,
                    ReviewState: item.reviewState,
                    CustomerVerify: item.customerVerify
                });
                this.isBusy = false;

            })
            .catch((err) => {
                this.isError = true;
            })
    },
    watch: {
        'inputData.ExCustomerName': function () {
            if (this.inputData.ExCustomerName == '') {
                this.inputDataCheck.NameError = true;
                this.inputDataCheck.NameErrorMsg = '姓名不得為空';
            }
            else {
                this.inputDataCheck.NameError = false;
                this.inputDataCheck.NameErrorMsg = '';
            }
        },
        'inputData.ExCustomerPhone': function () {
            let numberRegexp = /^[0-9]*$/;
            let phoneRegexp = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/
            if (!numberRegexp.test(this.inputData.ExCustomerPhone)) {
                this.inputDataCheck.PhoneError = true;
                this.inputDataCheck.PhoneErrorMsg = '必須為數字';
            }
            else if (this.inputData.ExCustomerPhone == '') {
                this.inputDataCheck.PhoneError = true;
                this.inputDataCheck.PhoneErrorMsg = '電話不得為空';
            } else if (!phoneRegexp.test(this.inputData.ExCustomerPhone)) {
                this.inputDataCheck.PhoneError = true;
                this.inputDataCheck.PhoneErrorMsg = '請輸入正確電話格式，例:0923463864';
            }
            else {
                this.inputDataCheck.PhoneError = false;
                this.inputDataCheck.PhoneErrorMsg = '';
            }
        },
        'inputData.ExCustomerEmail': function () {
            let emailRegexp = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z]+$/;
            if (this.inputData.ExCustomerEmail == '') {
                this.inputDataCheck.EmailError = true;
                this.inputDataCheck.EmailErrorMsg = '電子郵件不得為空';
            } else if (!emailRegexp.test(this.inputData.ExCustomerEmail)) {
                this.inputDataCheck.EmailError = true;
                this.inputDataCheck.EmailErrorMsg = "請輸入正確電子郵件格式，例:abc@gmail.com";
            }
            else {
                this.inputDataCheck.EmailError = false;
                this.inputDataCheck.EmailErrorMsg = '';
            }
        },
        'inputData.ExhibitionPrice': function () {
            let numberRegexp = /^[0-9]*$/;
            if (!numberRegexp.test(this.inputData.ExhibitionPrice)) {
                this.inputDataCheck.PriceError = true;
                this.inputDataCheck.PriceErrorMsg = "必須為數字";
            } else if (this.inputData.ExhibitionPrice == '' && this.inputData.ExhibitionPrice !=0) {
                this.inputDataCheck.PriceError = true;
                this.inputDataCheck.PriceErrorMsg = '票價不得為空';
            }
            else {
                this.inputDataCheck.PriceError = false;
                this.inputDataCheck.PriceErrorMsg = '';
            }
        },
        'inputData.ExhibitionStartTime': function () {
            if (this.inputData.ExhibitionStartTime == '') {
                this.inputDataCheck.StartTimeError = true;
                this.inputDataCheck.StartTimeErrorMsg = '日期不得為空';
            }
            else {
                this.inputDataCheck.StartTimeError = false;
                this.inputDataCheck.StartTimeErrorMsg = '';
            }
        },
        'inputData.ExhibitionEndTime': function () {
            if (this.inputData.ExhibitionEndTime == '') {
                this.inputDataCheck.EndTimeError = true;
                this.inputDataCheck.EndTimeErrorMsg = '日期不得為空';
            }
            else {
                this.inputDataCheck.EndTimeError = false;
                this.inputDataCheck.EndTimeErrorMsg = '';
            }
        },
        'inputData.ExName': function () {
            if (this.inputData.ExName == '') {
                this.inputDataCheck.ExNameError = true;
                this.inputDataCheck.ExNameErrorMsg = '展覽名稱不得為空';
            } else if (this.inputData.ExName.length > 50) {
                this.inputDataCheck.ExNameError = true;
                this.inputDataCheck.ExNameErrorMsg = '最多輸入50字';
            }
            else {
                this.inputDataCheck.ExNameError = false;
                this.inputDataCheck.ExNameErrorMsg = '';
            }
        },
        'inputData.MasterUnit': function () {
            if (this.inputData.MasterUnit == '') {
                this.inputDataCheck.MasterUnitError = true;
                this.inputDataCheck.MasterUnitErrorMsg = '主辦單位不得為空';
            } else if (this.inputData.MasterUnit.length > 50) {
                this.inputDataCheck.MasterUnitError = true;
                this.inputDataCheck.MasterUnitErrorMsg = '最多輸入50字';
            }
            else {
                this.inputDataCheck.MasterUnitError = false;
                this.inputDataCheck.MasterUnitErrorMsg = '';
            }
        }
    },
    methods: {
        modify(event) {
            this.$data.inputData.isValid = !this.$data.inputData.isValid
            this.$data.disabled = false
            let startDate = new Date(this.inputData.RentalDate.startDate)
            let endDate = new Date(this.inputData.RentalDate.endDate)

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
                let text = `${dateArray[i].getFullYear()}年${dateArray[i].getMonth() + 1}月${dateArray[i].getDate()}日`
                let options = { value: value, text: text };
                optionsArray.push(options);
            }

            this.startDateOptions = optionsArray;
            this.endDateOptions = '';
            this.inputData.ExhibitionStartTime = '';
            this.inputData.ExhibitionEndTime = '';

            let start = document.getElementById('startDate');
            start.addEventListener('change', (event) => {
                let endDateArray = new Array();
                for (i = 0; i < optionsArray.length; i++) {
                    if (event.currentTarget.value <= optionsArray[i].value) {
                        endDateArray.push(optionsArray[i]);
                    }
                }
                this.endDateOptions = endDateArray;
                
            })

        },
        modifySubmit: function () {
            //客戶回覆狀態變更
            let customerVerify = this.$data.inputData.CustomerVerify;
            if (customerVerify == false) {
                customerVerify = this.$data.inputData.CustomerVerify = !this.$data.inputData.CustomerVerify
            }
            var fd = new FormData();
            fd.append("ExhibitionId", form.$data.inputData.ExhibitionId);
            fd.append("ExCustomerId", form.$data.inputData.ExCustomerId);
            fd.append("ExCustomerName", document.getElementById("exCustomerName").value);
            fd.append("ExCustomerPhone", document.getElementById("exCustomerPhone").value);
            fd.append("ExCustomerEmail", document.getElementById("exCustomerEmail").value);
            fd.append("ExhibitionPrice", document.getElementById("exhibitionPrice").value);
            fd.append("ExhibitionStartTime", document.getElementById("startDate").value);
            fd.append("ExhibitionEndTime", document.getElementById("endDate").value);
            fd.append("ExName", document.getElementById("exName").value);
            fd.append("MasterUnit", document.getElementById("masterUnit").value);
            fd.append("ExPhoto", form.$data.inputData.ExPhoto);
            fd.append("ExhibitionIntro", document.getElementById("textarea-rows").value);
            fd.append("CustomerVerify", customerVerify);
            fd.append("ReviewState", form.$data.inputData.ReviewState);

            var isVaild = true;
            var keys = Object.keys(this.inputDataCheck);
            for (var keyIndex in keys) {
                if (typeof this.inputDataCheck[keys[keyIndex]] === 'boolean' && this.inputDataCheck[keys[keyIndex]] === true) {
                    isVaild = false;
                    break;
                }
            }

            if (isVaild) {

                axios({
                    method: 'POST',
                    data: fd,
                    url: '/api/Exhibiton/ModifyConfirm',
                })
                    .then((res) => {
                        if (res.data.isSuccess == true) {
                            form.$data.successShowMsg = "已修改，後面會再與您確認展演內容!"
                            form.$data.successShow = true;
                            setTimeout(function () {
                                form.$data.successShow = false;
                            }, 2000)
                            this.$data.disabled = true;
                            this.$data.btnDisabled = true;
                        }
                    })
                    .catch((err) => {
                        form.$data.dangerShow = true;
                        setTimeout(function () {
                            form.$data.dangerShow = false;
                        }, 2000);
                    })
            }
        },
        Submit: function () {
            let customerVerify = this.$data.inputData.CustomerVerify;
            let reviewState = this.$data.inputData.ReviewState
            if (customerVerify == false) {
                customerVerify = this.$data.inputData.CustomerVerify = !this.$data.inputData.CustomerVerify
            }
            if (reviewState == false) {
                reviewState = this.$data.inputData.ReviewState = !this.$data.inputData.ReviewState
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
                        form.$data.successShowMsg="已確認，已上架於展演區!"
                        form.$data.successShow = true;
                        setTimeout(function () {
                            form.$data.successShow = false;
                        }, 2000)
                        this.$data.disabled = true;
                        this.$data.btnDisabled = true;
                    }

                })
                .catch((err) => {
                    form.$data.dangerShow = true;
                    setTimeout(function () {
                        form.$data.dangerShow = false;
                    }, 2000);
                })
        },
        cancel: function () {
            location.href = `https://libreriaadmin.azurewebsites.net/Exhibiton/email?exhibitionId=${exhibitionId}`
        },
        photo: function () {
            var tmp = this;
            let exPhoto = document.getElementById("exPhoto")
            let img = $('.email-pic img')[0];
            let reader = new FileReader;
            if (exPhoto.files.length > 0) {
                reader.readAsDataURL(exPhoto.files[0]);
            } else {
                ExPhoto =  form.$data.inputData.ExPhoto
            }
            reader.onload = function (e) {
                let photoRegexp = /^.+\.(jpe?g|gif|png|bmp)$/i;
                if (!photoRegexp.test(exPhoto.value)) {
                    img.style["display"] = "none";
                    tmp.inputDataCheck.PhotoError = true;
                    tmp.inputDataCheck.PhotoErrorMsg = '這不是圖片檔!';
                }
                else {
                    img.style["display"] = "block";
                    tmp.inputDataCheck.PhotoError = false;
                    tmp.inputDataCheck.PhotoErrorMsg = '';
                    img.src = e.target.result;
                    ExPhoto = e;
                }

            };
        }
    }
});