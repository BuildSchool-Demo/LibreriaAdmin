let rental = new Vue({
    el: "#exhibitonApp",
    data() {
        return {
            items: [
                { isActive: true, exStartTime: '2021/02/03', exEndTime: '2021/03/01', exName: '一二三', ExPrice: 100, masterUnit: '234252', detail: { exPhoto: 'https://i.imgur.com/5hVqx53.jpg', exIntro: '3232546' }},
                { isActive: false, exStartTime: '2021/03/03', exEndTime: '2021/04/01', exName: '四五六' },
                { isActive: false, exStartTime: '2021/04/03', exEndTime: '2021/05/01', exName: '七八九' },
                { isActive: true, exStartTime: '2021/05/03', exEndTime: '2021/06/01', exName: 'aaaa' },
                { isActive: false, exStartTime: '2021/06/03', exEndTime: '2021/07/01', exName: 'bbbb' },
                { isActive: true, exStartTime: '2021/08/03', exEndTime: '2021/09/01', exName: 'cccc' },
                { isActive: true, exStartTime: '2021/09/03', exEndTime: '2021/10/01', exName: 'dddd' },
                { isActive: true, exStartTime: '2021/10/03', exEndTime: '2021/11/01', exName: 'eeee' },
                { isActive: true, exStartTime: '2021/11/03', exEndTime: '2021/12/01', exName: 'ffff' },
                { isActive: true, exStartTime: '2021/12/03', exEndTime: '2022/01/01', exName: 'gggg' },
                { isActive: true, exStartTime: '2022/01/03', exEndTime: '2022/02/01', exName: 'hhhhh' }
            ],
            fields: [
                { key: 'masterUnit', label: '主辦單位', sortable: true, sortDirection: 'desc' },
                { key: 'exName', label: '展覽名稱', sortable: true, sortDirection: 'desc' },
                { key: 'exStartTime', label: '展覽開始日期', sortable: true, class: 'text-center' },
                { key: 'exEndTime', label: '展覽結束日期', sortable: true, class: 'text-center' },
                { key: 'ExPrice', label: '門票', sortable: true, class: 'text-center' },
                { key: 'actions', label: '展演簡介' },
                { key: 'imge', label: '展演圖片' },
                {
                    key: 'isActive',
                    label: '審核狀態',
                    formatter: (value, key, item) => {
                        return value ? '已審核' : '尚未完成'
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
            infoModal: {
                id: 'info-modal',
                title: '',
                content: ''
            }
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
        info(item, index, button) {
            this.infoModal.title = `Row index: ${index}`
            this.infoModal.content = JSON.stringify(item, null, 2)
            this.$root.$emit('bv::show::modal', this.infoModal.id, button)
        },
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