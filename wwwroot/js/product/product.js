var productAPP = new Vue({
    el: '#product-index',
    data(){
        return {
            text: 'productName',
            items: [],
            fields: [
                { key: 'productId', label: '編號', sortable: true, sortDirection: 'desc' },
                { key: 'productName', label: '書名', sortable: true, sortDirection: 'desc' },
                { key: 'inventory', label: '庫存', sortable: true, sortDirection: 'desc' },
                { key: 'totalSales', label: '銷量', sortable: true, sortDirection: 'desc' },
                { key: 'isSpecial', label: '折扣', sortable: true, sortDirection: 'desc' },
                { key: 'unitPrice', label: '價格', sortable: true, sortDirection: 'desc' },
                { key: 'actions', label: '執行' },
            ],
            types: [
                'productId',
                'categoryId',
                'supplierId',
                'author',
                'publishDate',
                'productName',
                'inventory',
                'totalSales',
                'isSpecial',
                'unitPrice',
                'isbn',
                'mainUrl',
                'secondUrl',
                'thirdUrl',
                'fourthUrl',
                
            ],
            introduction:'',
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
        axios.get("/api/Product/GetAll")
            .then((res) => {
                this.items = res.data.body.productList;
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
        removeItem: function (item) {
            let backendApi = "https://localhost:5001/api/Product/DeleteItem";
            let result = document.getElementById("result");
            if ((item != null || item != '') && item > 0) {
                document.getElementById("apiUrl").innerText = "BackendAPI URL : " + backendApi;
                let request = {ProductId: item}
                $.ajax({
                    url: backendApi,
                    method: "POST",
                    dataType: "json",  
                    data: JSON.stringify(request),
                    contentType: "application/json;charset=UTF-8",   
                    success: function (data, textStatus, jqXHR) {
                        console.log(data);
                        console.log(textStatus);
                        console.log(jqXHR.status);
                        console.log(jqXHR.getAllResponseHeaders());
                        if (jqXHR.status == "202") {
                            result.innerText = `${item}資料刪除成功!`;
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        result.innerText = textStatus + ", " + jqXHR.status;
                    }
                })
            }

        }
        ,
        addItem: function () {
            alert('Hi')
            let backendApi = "https://localhost:5001/api/Product";
            let result = document.querySelector(".result");
            let categoryId = document.getElementById("type-categoryId").value;
            let productId = document.getElementById("type-productId").value;
            let supplierId = document.getElementById("type-supplierId").value;
            let author = document.getElementById("type-author").value;
            let publishDate = document.getElementById("type-publishDate").value;
            let productName = document.getElementById("type-productName").value;
            let inventory = document.getElementById("type-inventory").value;
            let totalSales = document.getElementById("type-totalSales").value;
            let isSpecial = document.getElementById("type-isSpecial").value;
            let unitPrice = document.getElementById("type-unitPrice").value;
            //let mainUrl = document.getElementById("type-mainUrl").value;
            //let secondUrl = document.getElementById("type-secondUrl").value;
            //let thirdUrl = document.getElementById("type-thirdUrl").value;
            //let fourthUrl = document.getElementById("type-fourthUrl").value;
            let preview = document.getElementById("type-introduction").value;
            let isbn = document.getElementById("type-isbn").value;

            let product = {
                CategoryId: categoryId, ProductId: productId, Supplier: supplierId,
                Author: author, PublishDate: publishDate, ProductName: productName, Inventory: inventory,
                TotalSales: totalSales, isSpecial: isSpecial, UnitPrice: unitPrice,
                Introduction: preview, isbn: isbn
            };
            console.log(product);
            $.ajax({
                url: backendApi,
                method: "POST",
                dataType: "json",  
             
               //如果有JSON回傳資料, 則加上此行, 若無請勿加, 會引起parse error
                contentType: "application/json;charset=UTF-8",    //Web API需配合[FromBody]
                data: JSON.stringify(product),
                success: function (data, textStatus, jqXHR) {
                    console.log(data);
                    console.log(textStatus);
                    console.log(jqXHR.getAllResponseHeaders());
                    console.log(jqXHR.getResponseHeader('location'));
                    result.innerText = `Status : ${textStatus}資料新增成功, location : ${jqXHR.getResponseHeader('location')}`;
                    productsList.innerText = JSON.stringify(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    result.innerText = textStatus + ", " + jqXHR.status;
                }
            })
           
             
    }
        


        
    }

});
