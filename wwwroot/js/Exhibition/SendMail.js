let Vm = new Vue ({
    el:'#sendMailapp',
    data() {
        return {
            form: {
                sender: '',
                recipient: '',
                subject: '',
                body: '',
                
            },
            show: true
        }
    },
    methods: {
        onSubmit(event) {
            event.preventDefault();
            let formData = JSON.stringify(this.form);
            this.$http.post('', formData).then(function (res) {
                if (res.status == 200) {
                    alert("成功")
                }
                else {
                    alert("失敗")
                }
            })
            
        },
        onReset(event) {
            event.preventDefault()
            // Reset our form values
            this.form.sender = ''
            this.form.recipient = ''
            this.form.subject = ''
            this.form.body = ''
            // Trick to reset/clear native browser form validation state
            //this.show = false
            //this.$nextTick(() => {
            //    this.show = true
            //})
        }
    }
})