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
            filterOn: []
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
        onFiltered(filteredItems) {
            // Trigger pagination to update the number of buttons/pages due to filtering
            this.totalRows = filteredItems.length
            this.currentPage = 1
        },
        createItem(item) {
            let backendApi = "api/Manager/CreateManager";
            $.ajax({
                url: backendApi,
                method: "POST",
                dataType: "json",
                contentType: "application/json;charset=UTF-8",
                data: JSON.stringify(product),
                success: function (data, textStatus, jqXHR) {
                    item.ManagerUserName = data.ManagerUserName;
                    item.ManagerName = data.ManagerName;
                    item.ManagerPassword = data.ManagerPassword;
                    item.managerRoleID = data.managerRoleID;

                    result.innerText = `Status : ${textStatus}資料新增成功, location : ${jqXHR.getResponseHeader('location')}`;
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    result.innerText = textStatus + "," + jqXHR.state;
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
       
    }
    });
