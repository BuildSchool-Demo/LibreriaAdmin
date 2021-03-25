var member = new Vue({
    el: "#member",
    data: {

        isError: false,
        isBusy: true,
        items: [],
        fields: [
            { key: 'memberId', label: '編號', sortable: true, sortDirection: 'desc' },
            { key: 'memberUserName', label: '會員名稱', class: 'text-center' },
            { key: 'idnumber', label: '身分證字號', class: 'text-center' },
            { key: 'memberName', label: '帳號', class: 'text-center' },
            { key: 'homeNumber', label: '電話', class: 'text-center' },
            { key: 'mobileNumber', label: '手機號碼', class: 'text-center' },
            { key: 'completeAddress', label: '住址', class: 'text-center' },
            { key: 'email', label: '信箱', class: 'text-center mg-right:50px' },
            { key: 'ordersum', label: '消費金額', sortable: true, class: 'text-right' },
            { key: 'actions', label: '編輯', class: 'text-center' },
        ],
        totalRows: 1,
        currentPage: 1,
        perPage: 20,
        pageOptions: [5, 10, 20, { value: 100, text: "Show a lot" }],
        sortBy: '',
        sortDesc: false,
        sortDirection: 'asc',
        filter: null,
        filterOn: [],
        infoModal: {
            id: 'info-modal',
            title: '',
            memberId: 0,
            memberUserName: '會員名稱',
            IDnumber: '身分證字號',
            memberName: '帳號',
            homeNumber: '電話',
            mobileNumber: '手機號碼',
            city: '縣市',
            region: '地區',
            address: '住址',
            email: 'email',


        },
        removeMessageBox: {
            item: null,
            index: -1,
        }
    },
    created: function () {
        axios.get("/Api/Member/GetAll")
            .then((res) => {
                console.log(res);
                this.items = res.data.body.memberList;
                this.mounted();
                this.isBusy = false;
            });

    },
    computed: {
        sortOptions() {
            // Create an options list from our fields
            return this.fields
                .filter(f => f.sortable)
                .map(f => {
                    return { text: f.label, value: f.key }
                })
        }
    },
    mounted() {
        // Set the initial number of items
        this.totalRows = this.items.length
    },
    methods: {
        info(item, index, button) {
            this.infoModal.title = `編輯資料：${item.memberId}`;
            this.infoModal.index = (this.currentPage - 1) * this.perPage + index;
            this.infoModal.content = JSON.stringify(item, null, 2);
            this.infoModal.memberId = item.memberId;
            this.infoModal.memberUserName = item.memberUserName;
            this.infoModal.IDnumber = item.idnumber;
            this.infoModal.memberName = item.memberName;
            this.infoModal.homeNumber = item.homeNumber;
            this.infoModal.mobileNumber = item.mobileNumber;
            this.infoModal.city = item.city;
            this.infoModal.region = item.region;
            this.infoModal.address = item.address;
            this.infoModal.email = item.email;
            this.$root.$emit('bv::show::modal', this.infoModal.id, button);
        },

        resetInfoModal() {
            //this.infoModal.title = ''
            //this.infoModal.content = ''
        },
        onFiltered(filteredItems) {
            // Trigger pagination to update the number of buttons/pages due to filtering
            this.totalRows = filteredItems.length
            this.currentPage = 1
        },
        mounted() {
            // Set the initial number of items
            this.totalRows = this.items.length
        },
        submitEdit() {
            perPage: 20,
                item = this.items[this.infoModal.index];
            item.memberId = this.infoModal.memberId;
            item.memberUserName = this.infoModal.memberUserName;
            item.IDnumber = this.infoModal.IDnumber;
            item.memberName = this.infoModal.memberName;
            item.homeNumber = this.infoModal.homeNumber;
            item.mobileNumber = this.infoModal.mobileNumber;
            item.city = this.infoModal.city;
            item.region = this.infoModal.region;
            item.address = this.infoModal.address;
            item.email = this.infoModal.email;

            item.completeAddress = item.city + item.region + item.address;
            axios.post('/api/Member/Edit', item)
                .then((res) => {
                })
        },

        removeItem() {
            let backendApi = "https://localhost:5001/api/Member/DeleteItem";
            if (this.removeMessageBox.item.memberId >= 0) {
                let request = { MemberId: this.removeMessageBox.item.memberId }
                //console.log(request)
                $.ajax({
                    url: backendApi,
                    method: "POST",
                    dataType: "json",   //如果有JSON回傳資料, 則加上此行, 若無請勿加, 會引起parse error
                    data: JSON.stringify(request),
                    contentType: "application/json;charset=UTF-8",
                    success: function (isSuccess) {
                        if (isSuccess) {
                            member.items.splice(member.removeMessageBox.index, 1);
                            $('#exampleModal').modal('hide');
                            alert("資料刪除成功 !");
                        }
                        else {
                            $('#exampleModal').modal('hide');
                            alert("資料刪除失敗 !");
                            
                        }
                    },
                })
            }
        },

        deleteMember(item, index) {
            this.removeMessageBox.item = item;
            this.removeMessageBox.index = (this.currentPage - 1) * this.perPage + index;
        }

    },
})