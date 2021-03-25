var managerAPP = new Vue({
    el: '#manager-index',
    data() {
        return {
            items: [],
            fields: [
                { key: 'managerID', label: 'ID', sortable: true, sortDirection: 'desc' },
                { key: 'managerUserName', label: '姓名', sortable: true, sortDirection: 'desc' },
                { key: 'managerRoleID', label: '管理者層級', sortable: true, sortDirection: 'desc' },
                { key: 'actions', label: '執行' },
            ],
            totalRows: 1,
            currentPage: 1,
            perPage: 20,
            pageOptions: [5, 10, 15, { value: 100, text: "Show a lot" }],
            sortBy: '',
            sortDesc: false,
            sortDirection: 'asc',
            filter: null,
            filterOn: [],
            infoModal: {
                id: 'info-modal',
                title: '',
                content: ''
            },
            newinfoModal: {
                id: 'newinfo-modal',
                title: '',
                content: ''
            }
        }
    },
    created: function () {
        axios.get("/api/Manager/GetAllManagers")
            .then((res) => {
                this.items = res.data.body.managerList;
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

        resetInfoModal() {
            this.infoModal.title = ''
            this.infoModal.content = ''
        },
        resetnewInfoModal() {
            this.newinfoModal.title = ''
            this.newinfoModal.content = ''
        },
        onFiltered(filteredItems) {
            // Trigger pagination to update the number of buttons/pages due to filtering
            this.totalRows = filteredItems.length
            this.currentPage = 1
        },
        info(item, index, button) {
            this.infoModal.index = (this.currentPage - 1) * this.perPage + index
            this.infoModal.title = `編輯資料: 管理者${item.managerUserName}`
            this.infoModal.managerID = item.managerID
            this.infoModal.managerName = item.managerName
            this.infoModal.managerPassword = item.managerPassword
            this.infoModal.managerUserName = item.managerUserName
            this.infoModal.managerRoleID = item.managerRoleID
            this.infoModal.content = JSON.stringify(item, null, 2)
            this.$root.$emit('bv::show::modal', this.infoModal.id, button)
        },
        createItem(item) {
            let backendApi = "/api/Manager/CreateManager";
            let managerUserName = item.managerUserName
            let managername = item.managerName
            let managerPassword = item.password
            let managerRoleID = item.managerRoleID

            let manager = { managerUserName: managerUserName, managerName: managername, managerPassword: managerPassword, managerRoleID: managerRoleID }
            
            $.ajax({
                url: backendApi,
                method: "POST",
                dataType: "json",
                contentType: "application/json;charset=UTF-8",
                data: JSON.stringify(manager),
                success: function (data, textStatus, jqXHR) {

                }
            });
        },
        removeItem(item) {
            let backendApi = "/api/Manager/DeleteManager";
                $.ajax({
                    url: backendApi + "/" + item.managerID,
                    method: "POST",
                    success: function (data, textStatus, jqXHR) {
                        console.log(data);
                        console.log(textStatus);
                        console.log(jqXHR.status);
                        console.log(jqXHR.getAllResponseHeaders());
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                    }
                })            
        },
        submitEdit(item) {
            perPage: 20, item = this.items[this.infoModal.index];
            item.managerID = this.infoModal.managerID;
            item.managerName = this.infoModal.managerName;
            item.managerPassword = this.infoModal.managerPassword;
            item.managerUserName = this.infoModal.managerUserName;
            item.managerRoleID = this.infoModal.managerRoleID;


            let backendApi = "/api/Manager/EditManager";
            $.ajax({
                url: backendApi + "/" + item.managerID,
                method: "POST",
                contentType: "application/json;charset=UTF-8",
                data: JSON.stringify(item),
                success: function (data, textStatus, jqXHR) {
                    console.log(data);
                    console.log(textStatus);
                    console.log(jqXHR.status);
                    console.log(jqXHR.getAllResponseHeaders());
                },
                error: function (jqXHR, textStatus, errorThrown) {
                }
            })
        },
       
    }
    });
