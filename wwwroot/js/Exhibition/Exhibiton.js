let rental = new Vue({
    el: "#exhibitonApp",
    data: {
        isError: false,
        isBusy: true,
        successShow: false,
        dangerShow: false,
        items: [],
        fields: [
            { key: 'masterUnit', label: '主辦單位', sortable: true, sortDirection: 'desc', class: 'text-center' },
            { key: 'exName', label: '展覽名稱', sortable: true, sortDirection: 'desc', class: 'text-center' },
            { key: 'exhibitionStartTime', label: '展覽開始日期', sortable: true, class: 'text-center' },
            { key: 'exhibitionEndTime', label: '展覽結束日期', sortable: true, class: 'text-center' },
            { key: 'exhibitionPrice', label: '門票', sortable: true, class: 'text-center' },
            { key: 'exhibitionIntro', label: '展演簡介', class: 'text-center' },
            { key: 'email', label: '寄信', class: 'text-center' },
            {
                key: 'reviewState',
                label: '審核狀態',
                class: 'text-center',
                formatter: (value, key, item) => {
                    return value ? '已審核' : '尚未完成'
                },
                sortable: true,
                sortByFormatted: true,
                filterByFormatted: true
            },
            {
                key: 'customerVerify',
                label: '客人回覆',
                class: 'text-center',
                formatter: (value, key, item) => {
                    return value ? '已回覆' : '尚未回覆'
                },
                sortable: true,
                sortByFormatted: true,
                filterByFormatted: true
            },
            { key: 'delete', label: '', class: 'text-center' }

        ],
        totalRows: 1,
        currentPage: 1,
        perPage: 10,
        sortBy: '',
        sortDesc: false,
        filter: null,

    },
    created: function () {
        axios.get("/api/Exhibiton/GetExhibitonData")
            .then((res) => {
                this.items = res.data.body.exhibitonList;
                this.isBusy = false;
            })
            .catch((err) => {
                this.isError = true;
            })
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

        email(item, button) {
            location.href = '/Exhibiton/SendMail?exhibitionId=' + item.exhibitionId;
        },
        onFiltered(filteredItems) {
            // Trigger pagination to update the number of buttons/pages due to filtering
            this.totalRows = filteredItems.length
            this.currentPage = 1
        },
        showMsgBoxTwo(target) {
            this.$bvModal.msgBoxConfirm('請確認是否刪除?', {
                title: '',
                size: 'sm',
                buttonSize: 'sm',
                okVariant: 'danger',
                okTitle: '確認',
                cancelTitle: '取消',
                footerClass: 'p-2',
                hideHeaderClose: false,
                centered: true
            })
                .then(value => {
                    if (value == true) {
                        let index = rental.$data.items.indexOf(target);

                        let result = {
                            IsDeleted: rental.$data.items[index].isDeleted = true,
                            ExhibitionId: rental.$data.items[index].exhibitionId,
                            ExCustomerId: rental.$data.items[index].exCustomerId,
                            IsCanceled: true
                        }

                        axios({
                            method: "POST",
                            data: result,
                            url:"/api/Exhibiton/IsDeleted"
                        })
                            .then((res) => {
                                if (res.data.isSuccess == true) {
                                    rental.$data.items.splice(index, 1);
                                    rental.$data.successShow = true;
                                    setTimeout(function () {
                                        rental.$data.successShow = false;
                                    },2000)
                                }
                            })
                            .catch((err) => {
                                rental.$data.dangerShow = true;
                                setTimeout(function () {
                                    rental.$data.dangerShow = false;
                                }, 2000);
                            })
                    }
                })
                .catch(err => {
                    rental.$data.dangerShow = true;
                    setTimeout(function () {
                        rental.$data.dangerShow = false;
                    }, 2000);
                })
        }
    }
})