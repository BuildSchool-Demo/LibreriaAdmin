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
        endDateOptions: []

    },
    created: function () {
        
        axios.get("/api/Exhibiton/EmailGetAll/" + exhibitionId)
            .then((res) => {
                console.log(res);
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
                    CustomerVerify: item.CustomerVerify
                });

            })
    },
    watch: {
        'inputData.ExCustomerName': function () {
            //immediate: true,
            if (this.inputData.ExCustomerName == '') {
                this.inputDataCheck.NameError = true;
                this.inputDataCheck.NameErrorMsg = '姓名不得為空';
            }
            else {
                this.inputDataCheck.NameError = false;
                this.inputDataCheck.NameErrorMsg = '';
            }
            //else if (this.inputData.Account.length < 8) {
            //    this.inputDataCheck.AccountError = true;
            //    this.inputDataCheck.AccountErrorMsg = '帳號長度不可小於八碼';
            //}

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
            } else if (this.inputData.ExhibitionPrice == '') {
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
        },
        //'inputData.ExPhoto': function () {
        //    let photo = document.getElementById("exPhoto").value;
        //    let photoRegexp = /^.+\.(jpe?g|gif|png)$/i;
        //    if (!photoRegexp.test(photo)) {
        //        let img = $('.email-pic img')[0];
        //        img.src = "https://imgur.com/vn1jhtP.jpg";
        //        this.inputDataCheck.PhotoError = true;
        //        this.inputDataCheck.PhotoErrorMsg = '這不是圖片檔!';

        //    }
        //    else {
        //        this.inputDataCheck.PhotoError = false;
        //        this.inputDataCheck.PhotoErrorMsg = '';
        //    }
        //}
    },
    methods: {
        //modify(event) {
        //    this.$data.inputData.isValid = !this.$data.inputData.isValid
        //    let startDate = new Date(this.inputData.RentalDate.startDate)
        //    let endDate = new Date(this.inputData.RentalDate.endDate)

        //    Date.prototype.addDays = function (days) {
        //        let dat = new Date(this.valueOf())
        //        dat.setDate(dat.getDate() + days);
        //        return dat;
        //    }

        //    function getDates(startDate, endDate) {
        //        let dateArray = new Array();
        //        let currentDate = startDate;
        //        while (currentDate <= endDate) {
        //            dateArray.push(currentDate)
        //            currentDate = currentDate.addDays(1);
        //        }
        //        return dateArray;
        //    }

        //    let dateArray = getDates(startDate, endDate);

        //    let optionsArray = new Array();
        //    for (i = 0; i < dateArray.length; i++) {
        //        let value = dateArray[i].yyyymmdd();
        //        let text = `${dateArray[i].getFullYear()}年${dateArray[i].getMonth()+1}月${dateArray[i].getDate()}日`
        //        let options = { value: value, text: text };
        //        optionsArray.push(options);
        //    }

        //    this.startDateOptions = optionsArray;
        //    this.endDateOptions = '';

        //    let start = document.getElementById('startDate');
        //    start.addEventListener('change', (event) => {
        //        let endDateArray = new Array();
        //        for (i = 0; i < optionsArray.length; i++) {
        //            if (event.currentTarget.value <= optionsArray[i].value) {
        //                endDateArray.push(optionsArray[i])
        //            }
        //        }
        //        this.endDateOptions = endDateArray;
        //    })
        //},
        modifySubmit: function () {
            this.$data.inputData.isValid = !this.$data.inputData.isValid

            //客戶回覆狀態變更
            let customerVerify = this.$data.inputData.CustomerVerify;
            if (customerVerify == false) {
                customerVerify = this.$data.inputData.CustomerVerify = !this.$data.inputData.CustomerVerify
            }


            let result = {
                ExhibitionId: form.$data.inputData.ExhibitionId,
                ExCustomerId: form.$data.inputData.ExCustomerId,
                ExCustomerName: document.getElementById("exCustomerName").value,
                ExCustomerPhone: document.getElementById("exCustomerPhone").value,
                ExCustomerEmail: document.getElementById("exCustomerEmail").value,
                ExhibitionPrice: document.getElementById("exhibitionPrice").value,
                ExhibitionStartTime: document.getElementById("startDate").value,
                ExhibitionEndTime: document.getElementById("endDate").value,
                ExName: document.getElementById("exName").value,
                MasterUnit: document.getElementById("masterUnit").value,
                ExPhoto: form.$data.inputData.ExPhoto,
                ExhibitionIntro: document.getElementById("textarea-rows").value,
                CustomerVerify: customerVerify,
                ReviewState: form.$data.inputData.ReviewState,
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
            var tmp = this;
            let exPhoto = document.getElementById("exPhoto")
            let img = $('.email-pic img')[0];
            let reader = new FileReader;
            if (exPhoto.files.length > 0) {
                reader.readAsDataURL(exPhoto.files[0]);
            } else {
                //ExPhoto: exhibitionData.EmailList[0].ExPhoto
            }
            reader.onload = function (e) {
                let photoRegexp = /^.+\.(jpe?g|gif|png)$/i;
                if (!photoRegexp.test(exPhoto)) {
                    img.src = "https://imgur.com/vn1jhtP.jpg";
                    tmp.inputDataCheck.PhotoError = true;
                    tmp.inputDataCheck.PhotoErrorMsg = '這不是圖片檔!';
                }
                else {
                    img.src = e.result;
                    ExPhoto = e;
                }

            };
        }
    }
});