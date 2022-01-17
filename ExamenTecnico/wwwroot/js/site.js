function updateSate(id) {
    var element = document.getElementById(id)
    var body = {
        ActiveState: element.checked,
        Description: 'test'
    };
    sendHttpRequest('PUT', `contracts/${id}`, body)
}

function postContract() {
    var input = document.getElementById('contract-input');

    if (!validateContract(input.value)) {
        return;
    }

    var body = {
        ActiveState: false,
        Description: input.value
    };

    var request = sendHttpRequest('POST', 'contracts', body);

    if (request.status == 200) {
        console.log(JSON.parse(request.response))
        document.location.href = ROOT + "/contracts"
    }
    
}

var sendHttpRequest = (method, url, body = null) => {
    var request = new XMLHttpRequest();
    request.open(method, `https://localhost:44379/${url}`, false);
    request.setRequestHeader("Accept", "application/json");
    request.setRequestHeader("Content-type", "application/json")
    request.send(JSON.stringify(body));
    return request;
}

var validateContract = (value) => {
    if (!value) {
        alert("el contrato no puede estar vacio")
        return false;
    } else if (value.length > 150) {
        alert("el contrato no puede exceder los 150 caracteres")
        return false;
    }

    return true;
}

