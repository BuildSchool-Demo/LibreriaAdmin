let rental = new Vue({
    el:"#rentalApp",
    data() {
        return {
            items: [
                { isActive: true, startDate: '2021/02/03', endDate: '2021/03/01', name: '一二三', _rowVariant: 'success', detail: { '展覽名稱': '123', '展覽開始日期': '456', '展覽結束日期': '456', 'img': '456'} },
                { isActive: false, startDate: '2021/03/03', endDate: '2021/04/01', name: '四五六' },
                { isActive: false, startDate: '2021/04/03', endDate: '2021/05/01', name: '七八九' },
                { isActive: true, startDate: '2021/05/03', endDate: '2021/06/01', name: 'aaaa' },
                { isActive: false, startDate: '2021/06/03', endDate: '2021/07/01', name: 'bbbb' },
                { isActive: true, startDate: '2021/08/03', endDate: '2021/09/01', name: 'cccc' },
                { isActive: true, startDate: '2021/09/03', endDate: '2021/10/01', name: 'dddd' },
                { isActive: true, startDate: '2021/10/03', endDate: '2021/11/01', name: 'eeee' },
                { isActive: true, startDate: '2021/11/03', endDate: '2021/12/01', name: 'ffff' },
                { isActive: true, startDate: '2021/12/03', endDate: '2022/01/01', name: 'gggg' },
                { isActive: true, startDate: '2022/01/03', endDate: '2022/02/01', name: 'hhhhh' }
            ],
            fields: [
                { key: 'name', label: '客戶名稱', sortable: true, sortDirection: 'desc' },
                { key: 'startDate', label: '租借開始日期', sortable: true, class: 'text-center' },
                { key: 'endDate', label: '租借結束日期', sortable: true, class: 'text-center' },
                {
                    key: 'isActive',
                    label: '付款狀態',
                    formatter: (value, key, item) => {
                        return value ? '已付款' : '尚未付款'
                    },
                    sortable: true,
                    sortByFormatted: true,
                    filterByFormatted: true
                },
                { key: 'actions', label: '展演資料' }
            ],
            d: [
                {name:'u u u',phone:'23267788'}

            ],
            totalRows: 1,
            currentPage: 1,
            perPage: 10,
            //pageOptions: [5, 10, 15, { value: 100, text: "Show a lot" }],
            sortBy: '',
            sortDesc: false,
            //sortDirection: 'asc',
            filter: null,
            //filterOn: [],
            //infoModal: {
            //    id: 'info-modal',
            //    title: '',
            //    content: ''
            //}
        }
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
        //info(item, index, button) {
        //    this.infoModal.title = `Row index: ${index}`
        //    this.infoModal.content = JSON.stringify(item, null, 2)
        //    this.$root.$emit('bv::show::modal', this.infoModal.id, button)
        //},
        resetInfoModal() {
            this.infoModal.title = ''
            this.infoModal.content = ''
        },
        onFiltered(filteredItems) {
            // Trigger pagination to update the number of buttons/pages due to filtering
            this.totalRows = filteredItems.length
            this.currentPage = 1
        }
    }
})