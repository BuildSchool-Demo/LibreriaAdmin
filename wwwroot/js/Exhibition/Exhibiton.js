let rental = new Vue({
    el: "#exhibitonApp",
    data: {
        //isError: false,
        //isBusy: true,
        items: [
            //{ reply: true, ReviewState: true, ExhibitionStartTime: '2021/02/03', ExhibitionEndTime: '2021/03/01', MasterUnit: '一二三', ExhibitionPrice: 100, ExName: '234252', ExPhoto: 'https://i.imgur.com/5hVqx53.jpg', ExhibitionIntro: '3232546' },
            
        ],
        fields: [
            { key: 'masterUnit', label: '主辦單位', sortable: true, sortDirection: 'desc', class: 'text-center' },
            { key: 'exName', label: '展覽名稱', sortable: true, sortDirection: 'desc', class: 'text-center' },
            { key: 'exhibitionStartTime', label: '展覽開始日期', sortable: true, class: 'text-center' },
            { key: 'exhibitionEndTime', label: '展覽結束日期', sortable: true, class: 'text-center' },
            { key: 'exhibitionPrice', label: '門票', sortable: true, class: 'text-center' },
            { key: 'exhibitionIntro', label: '展演簡介', class: 'text-center' },
            { key: 'exPhoto', label: '展演圖片', class: 'text-center' },
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
            //{
            //    key: 'reply',
            //    label: '客人回覆',
            //    class: 'text-center',
            //    formatter: (value, key, item) => {
            //        return value ? '已回覆' : '尚未回覆'
            //    },
            //    sortable: true,
            //    sortByFormatted: true,
            //    filterByFormatted: true
            //},

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
    },
    created: function () {
        axios.get("/api/Exhibiton/GetExhibitonData")
            .then((res) => {
                console.log(res);
                this.items = res.data.body;
                this.isBusy = false;
            })
            //.catch((err) => {
            //    //this.isError = true;
            //    console.log(err);
            //})
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
        info(item, button) {
            this.infoModal.title = '展演圖片'
            this.infoModal.content = item.exPhoto
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