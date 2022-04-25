function HttpRequestToController(requestType, params, targetUrl) {
    return new Promise((resolve, reject) => {
        const xhr = new XMLHttpRequest();
        xhr.open(requestType, targetUrl, true);

        xhr.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');

        xhr.onreadystatechange = () => {
            if (xhr.readyState == 4 && xhr.status == 200) {
                resolve(xhr.responseText);
            }
        }

        xhr.onerror = () => reject(xhr.statusText);

        xhr.send(params);
    });
}

function GetSelectedProductValue() {
    let params = "id=" + document.getElementById("selectedProduct").value;
    let targetUrl = "/Product/GetProductValue";
    HttpRequestToController("POST", params, targetUrl)
        .then((response) => {
            document.getElementById("productValue").value = response;
    });
}