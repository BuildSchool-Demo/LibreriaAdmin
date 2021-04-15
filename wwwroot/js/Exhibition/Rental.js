let rental = new Vue({
    el:"#rentalApp",
    data() {
        return {
            isError: false,
            isBusy: true,
            items: [],
            fields: [
                { key: 'exCustomerName', label: '客戶姓名', sortable: true, sortDirection: 'desc', class: 'text-center'},
                { key: 'exCustomerPhone', label: '客戶電話', sortDirection: 'desc', class: 'text-center' },
                { key: 'exCustomerEmail', label: '客戶Email', sortDirection: 'desc', class: 'text-center' },
                { key: 'exhibitonData', label: '展演資料', class: 'text-center' },
                { key: 'startDate', label: '租借開始日期', sortable: true, class: 'text-center' },
                { key: 'endDate', label: '租借結束日期', sortable: true, class: 'text-center' },
                { key: 'price', label: '租借費用總額', sortable: true, class: 'text-center' },
                {
                    key: 'paymentState',
                    label: '付款狀態',
                    class: 'text-center',
                    formatter: (value) => {
                        return value ? '已付款' : '尚未付款'
                    },
                    sortable: true,
                    sortByFormatted: true,
                    filterByFormatted: true
                },
                {
                    key: 'isCanceled',
                    label: '訂單狀態',
                    class: 'text-center',
                    formatter: (value) => {
                        return value ? '取消' : '成立'
                    },
                    sortable: true,
                    sortByFormatted: true,
                    filterByFormatted: true
                }
            ],
            totalRows: 1,
            currentPage: 1,
            perPage: 10,
            sortBy: '',
            sortDesc: false,
            filter: null,
            //filterOn: [],
            //infoModal: {
            //    id: 'info-modal',
            //    title: '',
            //    content: ''
            //}
        }
    },
    created: function () {
        axios.get("/api/Exhibiton/GetRentalDate")
            .then((res) => {
                this.items = res.data.body.rentalList;
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
        resetInfoModal() {
            this.infoModal.title = ''
            this.infoModal.content = ''
        },
        onFiltered(filteredItems) {
            // Trigger pagination to update the number of buttons/pages due to filtering
            this.totalRows = filteredItems.length
            this.currentPage = 1
        },
        getExhibitonDataFieldName(fieldKey) {
            switch (fieldKey) {
                case 'exName':
                    return '展覽名稱';
                case 'exhibitionStartTime':
                    return '展覽開始時間';
                case 'exhibitionEndTime':
                    return '展覽結束時間';
                default:
                    return '';
            }
        }
    }
})