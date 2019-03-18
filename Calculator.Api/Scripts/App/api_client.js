$.ajaxSetup({
    beforeSend: function (xhr) {
        xhr.setRequestHeader("Accept", "application/json");
        xhr.setRequestHeader("Content-Type", "application/json");
    }
});

function ApiClient() {
    this.calculate = function (input, successCallback, failedCallback) {
        $.post("api/evaluator/evaluate",
            JSON.stringify({ expression: input }),
            successCallback,
            failedCallback);
    };
}