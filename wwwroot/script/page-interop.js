window.setTitle = (title) => {
    document.title = title;
}

window.addHeadChild = (element, attrs) => {
    var child = document.createElement(element);

    for (var key in attrs) {
        child.setAttribute(key, attrs[key]);
    }

    document.head.appendChild(child);
}