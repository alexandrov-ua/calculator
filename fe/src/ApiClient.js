class ApiClient {
    constructor() {
        this.baseUrl = "";

        $.ajaxSetup({
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Accept", "application/json");
                xhr.setRequestHeader("Content-Type", "application/json");

            }
        });
    }

    calculate(input, successCallback, failedCallback) {
        $.post(this.baseUrl + "/api/evaluator/evaluate",
            JSON.stringify({ expression: input }),
            successCallback,
            failedCallback);
    }

    getLog(successCallback, failedCallback) {
        $.get(this.baseUrl + "/api/evaluator/log",
            successCallback,
            failedCallback);
    }

    downloadLog(successCallback, failedCallback) {
        var link = document.createElement('a');
        link.href = this.baseUrl + "/api/evaluator/log-file";
        document.body.appendChild(link);
        link.click();
    }
}

export { ApiClient }