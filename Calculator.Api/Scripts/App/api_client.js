$.ajaxSetup({
    beforeSend: function (xhr) {
        xhr.setRequestHeader("Accept", "application/json");
        xhr.setRequestHeader("Content-Type", "application/json");
    }
});

function ApiClient() {
    this.calculate = function (input, successCallback, failedCallback) {
        $.post("/api/evaluator/evaluate",
            JSON.stringify({ expression: input }),
            successCallback,
            failedCallback);
    };

    this.getLog = function(successCallback, failedCallback) {
        $.get("/api/evaluator/log",
            successCallback,
            failedCallback);
    };

    this.downloadLog = function(successCallback, failedCallback) {
        var link = document.createElement('a');
        link.href = "/api/evaluator/log-file";
        document.body.appendChild(link);
        link.click();
    };
}