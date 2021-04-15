let vm = new Vue({
    el: '#sendMailapp',
    data: {
        successShow: false,
        dangerShow: false,
        form: {
            sender: 'LibreriaBSProject@gmail.com',
            recipient: '',
            subject: '',
            body: '',
            exhibitionId: exhibitionId
        },
        show: true,
        libreriaEmail: `https://libreriaadmin.azurewebsites.net/Exhibiton/Email?exhibitionId=${exhibitionId}`

    },
    created: function () {
        axios.get("/api/Exhibiton/GetByCustomerEmail/" + exhibitionId)
            .then((res) => {
                this.$data.form.recipient = res.data.body.exCustomerEmail
            })
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
                if (res.status == 200) {
                    vm.$data.successShow = true;
                    setTimeout(function () {
                        vm.$data.successShow = false;
                    }, 2000)
                    setTimeout(function () {
                        location.href = 'https://libreriaadmin.azurewebsites.net/Exhibiton/ExhibitonIndex'
                    },1500)
                }
                
            });

        },
        onReset(event) {
            event.preventDefault()
            // Reset our form values
            this.form.sender = ''
            this.form.recipient = ''
            this.form.subject = ''
            this.form.body = ''
        }
    }
})