let Vm = new Vue({
    el: '#sendMailapp',
    data() {
        return {
            form: {
                sender: 'LibreriaBSProject@gmail.com',
                recipient: '',
                subject: '',
                body: '',

            },
            show: true,
        }
    },
    methods: {
        onSubmit(event) {
            event.preventDefault();
            const options = {
                method: 'POST',
                headers: { 'content-type': 'application/json' },
                data: JSON.stringify(this.form),
                url: '/api/Exhibiton/SendMail',
            };
            axios(options).then((res) => {
                alert(res.data);
            });

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