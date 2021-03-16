var form = new Vue({
    el: '#email',
    data: {
        AddVerify: true,
        inputData: {
            Name: '',
            Tel: '',
            Email: '',
            Price: '',
            StartDate: '',
            EndDate: '',
            ExName: '',
            MasterUnit:'',
            Pic: '',
            Intro:''
        },
        inputDataCheck: {
            AccountError: false,
            AccountErrorMsg: '',
            PasswordError: false,
            PasswordErrorMsg: '',
            CheckPasswordError: false,
            CheckPasswordErrorMsg: '',
        },
        selected: null,
        options: [
            { value: null, text: 'Please select an option' },
            { value: 'a', text: 'This is First option' },
            { value: 'b', text: 'Selected Option', disabled: true }
        ]
    },
    watch: {
        'inputData.Account': {
            immediate: true,
            handler: function () {
                if (this.inputData.Account == '') {
                    this.inputDataCheck.AccountError = true;
                    this.inputDataCheck.AccountErrorMsg = '帳號不得為空';
                }
                else if (this.inputData.Account.length < 8) {
                    this.inputDataCheck.AccountError = true;
                    this.inputDataCheck.AccountErrorMsg = '帳號長度不可小於八碼';
                }
                else {
                    this.inputDataCheck.AccountError = false;
                    this.inputDataCheck.AccountErrorMsg = '';
                }
            }
        },
        'inputData.Password': function () {
            let passwordRegexp = /^[0-9]*$/;
            this.inputDataCheck.CheckPasswordError = false;
            this.inputDataCheck.CheckPasswordErrorMsg = '';
            if (!passwordRegexp.test(this.inputData.Password)) {
                this.inputDataCheck.PasswordError = true;
                this.inputDataCheck.PasswordErrorMsg = '密碼必須為數字';
            }
            else if (this.inputData.Password == '') {
                this.inputDataCheck.PasswordError = true;
                this.inputDataCheck.PasswordErrorMsg = '密碼不得為空';
            } else if (this.inputData.Password.length < 8) {
                this.inputDataCheck.PasswordError = true;
                this.inputDataCheck.PasswordErrorMsg = '密碼長度不可小於八碼';
            }
            else {
                this.inputDataCheck.PasswordError = false;
                this.inputDataCheck.PasswordErrorMsg = '';
            }
        },
        'inputData.CheckPassword': function () {
            if (this.inputData.CheckPassword != this.inputData.Password) {
                this.inputDataCheck.CheckPasswordError = true;
                this.inputDataCheck.CheckPasswordErrorMsg = '密碼必須相同';
            }
            else {
                this.inputDataCheck.CheckPasswordError = false;
                this.inputDataCheck.CheckPasswordErrorMsg = '';
            }
        }
    },
    methods: {
        checkAddVerify() {
            for (let index in this.inputDataCheck) {
                if (this.inputDataCheck[index] == true) {
                    thid.AddVerify = false;
                    return;
                }
            }
            this.AddVerify = true;
        }
    }
});