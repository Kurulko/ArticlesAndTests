let exercises = document.getElementsByClassName("exercise");
let count = 0;
for (let exercise of exercises) {
    let answers = exercise.getElementsByClassName("answer");
    for (let answer of answers) {

        let isClick = false;
        answer.addEventListener("click", (e) => {
            if (isClick)
                return;
            isClick = true;

            let target = e.target;
            if (target.value === "True")
                ++count;

            for (let _answer of answers) {
                if (answer !== _answer)
                    _answer.setAttribute("disabled", "disabled");
                else
                    if (_answer.value === "False")
                        _answer.parentNode.style.backgroundColor = "red";

                if (_answer.value === "True")
                    _answer.parentNode.style.backgroundColor = "green";
            }
        });
    }
}

let showResult = document.getElementById("showResult");
showResult.addEventListener("click", () => {
    let result = document.getElementById("result");
    result.innerText = `Result: ${count}`;
});
