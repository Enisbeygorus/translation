﻿function getSelect(name, parent) {
    if (!parent) parent = document;
    return parent.querySelector('#select-' + name);
}

function hasClass(element, className) { return (' ' + element.className + ' ').indexOf(' ' + className + ' ') > -1; }

function trimSearchTerm(term, selectContent) {

    let items = selectContent.childNodes;
    for (let i = 0, l = items.length; i < l; i++) {
        if (items[i]
            && items[i].innerText) {
            if (items[i].innerText.toLowerCase() === term.trim().toLowerCase()) {
                return '';
            }
        }
    }

    return term.trim().toLowerCase().replace(/<br>/g, '').replace(/&nbsp;/g, '');
}

let theSelectChangeEvent = new Event('theSelectChange', { bubbles: true });
function onSelectChange(name, onChange) {
    const select = typeof name === 'string' ? getSelect(name) : name;

    select.addEventListener('theSelectChange', function (e) {
        if (typeof name === 'string') {
            if (name !== select.dataset.valueField) {
                return;
            }
        }

        onChange(select, select.dataset.value, select.dataset.text);
    });
}

let theSelectClearEvent = new Event('theSelectClear');
let theSelectInitEvent = new Event('theSelectInit');

function onSelectClear(name, onClear) {
    let s = typeof name === 'string' ? getSelect(name) : name;
    s.addEventListener('theSelectClear', function (e) {
        if (typeof name === 'string') {
            if (name !== s.dataset.valueField) {
                return;
            }
        }

        onClear(s, s.dataset.value, s.dataset.text);
    });
}

theSelectInitEvent = new Event('theSelectInit');
function onSelectInit(name, cb) {
    let s = typeof name === 'string' ? getSelect(name) : name;
    s.addEventListener('theSelectInit', function (e) {
        if (typeof name === 'string') {
            if (name !== s.dataset.valueField) {
                return;
            }
        }

        cb(s, s.dataset.value, s.dataset.text);
    });
}

let urlArray = [];
setInterval(function () { urlArray = []; }, 13000);

let theTrue = 'True';
let theValueSeparator = ',';
let refreshSelect = () => { };
let initSelects = () => { };
let initSingleSelect = () => { };
let setSingleSelect = () => { };
let resetSingleSelect = () => { };
let initSelectContentFromJson = () => { };
let clearSelect = () => { };
let clearSelectContent = () => { };
let clearMultipleSelectContent = () => { };
let selectDataCache = {};
let onChangeSelect = () => { };
let onClearSelect = () => { };
let resetSelectWithoutCloning = () => { };

function createSelectElement(name, urlOrData, typeClass, contentType, value, text, isSetFirstItem, parent, initValue, onChange, isIconsVisible = false, iconFontSize = 20, onClear, confirmBeforeChange = false, confirmBeforeClear = false, checkForEmptyElementBeforeChange = '', emptyElementPropertyToCheck = '') {
    if (!typeClass) { typeClass = ['single-select']; }
    if (typeClass === 'radio-select') { typeClass = ['radio-select', 'single-select']; }
    if (typeof typeClass === 'string') { typeClass = [typeClass]; }
    let theSelect = document.createElement('div');
    theSelect.id = 'select-' + name;
    theSelect.name = 'select-' + name;
    theSelect.dataset.name = name;
    theSelect.dataset.valueField = name + 'Uid';
    theSelect.dataset.textField = name + 'Name';
    theSelect.dataset.value = value || '';
    theSelect.dataset.text = text || '';
    if (typeof urlOrData === 'string') {
        theSelect.dataset.url = urlOrData;
    } else if (urlOrData) {//[{text: string, value: string}]
        const jsonDataUid = `data-json-${guid()}`;
        theSelect.dataset.url = 'data-json';
        theSelect.dataset.json = jsonDataUid;
        selectDataCache[jsonDataUid] = { responseText: JSON.stringify(urlOrData) };
    }
    theSelect.dataset.parent = parent || '';
    theSelect.dataset.setFirstItem = isSetFirstItem === true ? theTrue : false;
    theSelect.dataset.type = contentType === undefined ? 'text' : contentType;
    theSelect.dataset.isIconsVisible = !!isIconsVisible + '';
    theSelect.dataset.iconFontSize = iconFontSize + 'px';
    theSelect.dataset.confirmBeforeChange = confirmBeforeChange;
    theSelect.dataset.confirmBeforeClear = confirmBeforeClear;
    theSelect.dataset.checkForEmptyElementBeforeChange = checkForEmptyElementBeforeChange;
    theSelect.dataset.emptyElementPropertyToCheck = emptyElementPropertyToCheck;
    theSelect.classList.add('select');
    theSelect.classList.add(...typeClass);
    initValue && setSingleSelect(theSelect, initValue);
    onChange && onSelectChange(theSelect, onChange);
    onClear && onSelectClear(theSelect, onClear);

    return theSelect;
}

(function () {
    let loadCount = 100;  // item count which load each time
    let oldSearchTerm = '';
    let isNotChanged = false;
    let allSelects = document.querySelectorAll('div.select');

    refreshSelect = function (s, cb) {
        if (hasClass(s, 'radio-select')) {
            if (s.dataset.setFirstItem === theTrue) {
                getSelectData(s, s.dataset.url + '?take=' + loadCount, function (req) {
                    selectDataFiller(s, req);
                    if (cb) cb();
                });
            }
        } else {
            if (s.dataset.value === ''
                && s.dataset.text === ''
                && s.dataset.setFirstItem === theTrue) {
                getSelectData(s, s.dataset.url + '?take=' + loadCount, function (req) {
                    selectDataFiller(s, req);
                    if (cb) cb();
                });
            } else {
                selectDataFiller(s);
                if (cb) cb();
            }
        }
    };

    function selectDataFiller(s, req) {
        let json = [];
        if (req !== undefined) { json = JSON.parse(req.responseText); }

        if (hasClass(s, 'radio-select')) {
            initRadioSelect(s, json);
        } else {
            if (hasClass(s, 'single-select')) { initSingleSelect(s, json); } else if (hasClass(s, 'multiple-select')) { initMultipleSelect(s, json); }
        }
    }

    function toggleSelectContent(selectButton) {
        let settingsParent = getParent(selectButton, 'ra-settings-wrapper');
        let theSelect = selectButton.closest('.select');
        if (hasClass(theSelect, 'show')) {
            theSelect.classList.remove('show');
            if (settingsParent) {
                settingsParent.style.overflow = 'hidden';
            }
        } else if (!hasClass(theSelect, 'disabled')) {
            theSelect.classList.add('show');
            if (settingsParent) {
                settingsParent.style.overflow = 'initial';
            }
            let parentValue = '';

            if (theSelect.dataset.parent !== ''
                && theSelect.dataset.parent !== undefined) {
                let parent = getSelect(theSelect.dataset.parent);

                if (parent !== undefined && parent !== null
                    && parent.dataset.value !== undefined) {
                    parentValue = parent.dataset.value;
                }
            }

            let lastUid = '';
            let itemCount = theSelect.lastElementChild.childElementCount;
            if (theSelect.lastElementChild.childNodes !== undefined
                && theSelect.lastElementChild.childNodes[itemCount] !== undefined
                && itemCount > 0) {
                lastUid = theSelect.lastElementChild.childNodes[itemCount].getAttribute('value');
            }

            let url = theSelect.dataset.url;
            if (url) {
                getSelectData(theSelect, url + '?take=' + loadCount + '&parent=' + parentValue + '&lastUid=' + lastUid, function (req) {
                    let json = JSON.parse(req.responseText);
                    updateSelectContent(theSelect, json);
                });
            }
        }
    }

    function initSelectItems(selectContent, item, type, theSelect) {
        let texts = theSelect.querySelector('#' + theSelect.dataset.textField).getAttribute('value');
        if (texts && texts.indexOf(item.text) >= 0) return null;

        let items = selectContent.childNodes;
        for (let i = 0, l = items.length; i < l; i++) {
            if (items[i].innerHTML === item.text) {
                return null;
            }
        }

        let selectItem = createElement('div');
        selectItem.className = 'select-item';

        if (type === 'content') {
            selectItem.classList.add('content-item');
            let img = createElement('img');
            img.className = 'select-item-image';
            img.setAttribute('src', item.image);
            let selectItemText = createElement('label');
            selectItemText.className = 'select-item-text';
            selectItemText.innerHTML = item.text;
            selectItem.appendChild(img);
            selectItem.appendChild(selectItemText);
        } else {
            selectItem.innerHTML = item.text;
            selectItem.setAttribute('title', item.text);
        }

        selectItem.setAttribute('value', item.value);
        selectContent.appendChild(selectItem);

        return selectItem;
    }

    function reFillChildsData(value, parentName) {
        const childrenSelects = document.querySelectorAll(`.select[data-parent='${parentName}']`);
        childrenSelects.forEach(function (s) {
            if (s.dataset.parent !== parentName) { return; }

            let url = s.dataset.url + '?take=' + loadCount + '&parent=' + value;
            if (!urlArray.includes(url)) {
                urlArray.push(url);
            }
            let select = s;
            getSelectData(s, url, function (req) {
                s.innerHTML = '';
                s.dataset.text = '';
                s.dataset.value = '';
                selectDataFiller(select, req);
            });
        });
    }

    function addSelectContent(selectContent) {
        //visible height + pixel scrolled = total height
        if (selectContent.offsetHeight + selectContent.scrollTop >= selectContent.scrollHeight) {
            let theSelect = selectContent.closest('.select');
            let parentId = theSelect.dataset.parent;
            let searchTerm = trimSearchTerm(theSelect.querySelector('.select-button-input').innerHTML, selectContent);

            let selectItems = selectContent.querySelectorAll('.select-item');
            let lastLength = selectItems.length;

            let selectUrl = theSelect.dataset.url;
            if (!selectUrl) return;

            let getUrl = selectUrl + '?q=' + searchTerm + '&take=' + loadCount;
            if (selectItems.length > 0) {

                let lastUid = selectItems[(lastLength - 1)].getAttribute('value');
                getUrl += '&lastUid=' + lastUid;
            }

            if (parentId) {
                let parent = getSelect(parentId);
                if (parent !== undefined) {
                    getUrl += '&parent=' + parent.dataset.value;
                }
            }

            if (!urlArray.includes(getUrl)) {
                urlArray.push(getUrl);
                getSelectData(theSelect, getUrl, function (req) {
                    let json = JSON.parse(req.responseText);
                    let type = theSelect.dataset.type;
                    for (let i = 0; i < json.length; i++) {
                        let selectItem = initSelectItems(selectContent, json[i], type, theSelect);
                        if (selectItem !== null) {
                            selectItem.onclick = function () {
                                if (hasClass(theSelect, 'single-select')) { onClickSingleSelectItem(this); } else { onClickMultipleSelectItem(this); }
                            };
                        }
                    }
                });
            }
        }
    }

    function updateSelectContent(theSelect, updateItem) {
        let selectContent = theSelect.querySelector('.select-content');
       

        if (updateItem.length === 0 && selectContent.childElementCount === 0) {
            selectContent.innerHTML = ' ';
            let noItem = createElement('div');
            noItem.style.padding = '5px';
            noItem.style.color = 'grey';
            noItem.innerHTML = 'no_item';
            noItem.dataset.translation = 'no_item';
            selectContent.appendChild(noItem);
        }

        for (let i = 0; i < updateItem.length; i++) {
            let selectItem = initSelectItems(selectContent, updateItem[i], theSelect.dataset.type, theSelect);
            if (selectItem !== null) {
                selectItem.onclick = function () {
                    if (hasClass(theSelect, 'single-select')) { onClickSingleSelectItem(this); } else if (hasClass(theSelect, 'multiple-select')) { onClickMultipleSelectItem(this); }
                };
            }
        }

        //reposition selectContent
        repositionSelectContent();
    }

    function searchSelectContent(selectInput, event) {
        let theSelect = selectInput.closest('.select');
        let selectButton = theSelect.querySelector('.select-button');
        let selectContent = theSelect.querySelector('.select-content');
        let parentId = theSelect.dataset.parent;
        let searchTerm = trimSearchTerm(selectInput.innerHTML, selectContent);

        // input empty and press backspace block request
        if (oldSearchTerm.length === 0 && event.keyCode === 8) {
            return;
        }
        // prevent request when pressed up arrow, down arrow, enter,
        if (event.keyCode === 38 ||
            event.keyCode === 40 ||
            event.keyCode === 13 ||
            searchTerm.length === 1) { return; }

        //prevent request when input value is not changed when press enter
        if (oldSearchTerm === selectInput.innerHTML) {
            isNotChanged = true;
            return;
        }
        oldSearchTerm = selectInput.innerHTML;
        isNotChanged = false;

        // delete existing loader
        if (theSelect.querySelector('.loader')) { theSelect.querySelector('.loader').remove(); }
        // create new loader
        let loader = createElement('div');
        loader.className = 'loader';
        loader.style.right = '-30px';
        selectButton.insertBefore(loader, selectButton.firstChild);

        let url = theSelect.dataset.url + '?q=' + searchTerm + '&take=' + loadCount;
        if (parentId) {
            let parent = getSelect('select-' + parentId);
            if (parent !== undefined
                && parent.dataset.value !== undefined) {
                url += '&parent=' + parent.dataset.value;
            }
        }
        // send request
        if (!urlArray.includes(url)) {
            urlArray.push(url);
            getSelectData(theSelect, url, function (req) {
                let updateItems = JSON.parse(req.responseText);
                if (theSelect.querySelector('.loader')) { theSelect.querySelector('.loader').remove(); }
                clearContent(theSelect);
                updateSelectContent(theSelect, updateItems);
            });
        }
    }

    // Single Select Functions
    setSingleSelect = function (theSelect, value, isSilent) {
        if (theSelect.dataset.value === value) return;
        if (!theSelect.dataset.url || theSelect.dataset.url === "undefined") return;

        getSelectData(theSelect, theSelect.dataset.url + '?take=' + loadCount, function (req) {
            const json = JSON.parse(req.responseText);
            updateSelectContent(theSelect, json);

            const item = theSelect.querySelector('[value="' + value + '"]');
            if (item) {
                onClickSingleSelectItem(item, isSilent);
            } else if (theSelect.dataset.setFirstItem === theTrue) {
                const $firstItem = theSelect.querySelectorAll('.select-content [value]')[0];
                $firstItem && onClickSingleSelectItem($firstItem, isSilent);
            } else {
                clearSelect(theSelect);
            }
        });

    };

    resetSingleSelect = function (theSelect) {
        theSelect.dataset.value = '';
        theSelect.dataset.text = '';
        theSelect.removeAttribute('data-is-valid');

        //clone select without children to empty inner html and remove any previous listeners
        let selectClone = theSelect.cloneNode(false);
        theSelect.parentNode.replaceChild(selectClone, theSelect);

        return selectClone;
    };

    initSingleSelect = function (theSelect, json) {
        return new Promise((resolve, reject) => {
            if (!hasClass(theSelect, 'radio-select') && theSelect.querySelector('div.select-button') === null) {
                initSingleSelectButton(theSelect, json);
                if (theSelect.dataset.detail === theTrue) {
                    initSingleSelectInfo(theSelect, theSelect.dataset.detailUrl);
                }
                initSingleSelectContent(theSelect, json);

                resolve(1);
            }
        });
    };

    function initSingleSelectButton(theSelect, json) {
        let selectButton = createElement('div');
        selectButton.className = 'select-button';

        let uidInput = createElement('input');
        uidInput.setAttribute('type', 'hidden');
        uidInput.id = theSelect.dataset.valueField;
        uidInput.name = theSelect.dataset.valueField;

        let textInput = createElement('input');
        textInput.setAttribute('type', 'hidden');
        textInput.id = theSelect.dataset.textField;
        textInput.name = theSelect.dataset.textField;

        let selectButtonInput = createElement('div');
        selectButtonInput.className = 'select-button-input';
        selectButtonInput.setAttribute('contenteditable', theTrue);
        let clearSelectButton = createButton('X', onClearButtonClick);
        clearSelectButton.className = 'clear-select-button';
        selectButton.innerHTML = '';
        selectButton.appendChild(uidInput);
        selectButton.appendChild(textInput);
        selectButton.appendChild(selectButtonInput);
        selectButton.appendChild(clearSelectButton);
        theSelect.appendChild(selectButton);
        if (json === undefined && theSelect.dataset.value === '') {
            selectButton.classList.add('disabled');
            return;
        }

        if (hasClass(selectButton, 'disabled')) {
            selectButton.classList.remove('disabled');
        }

        if (theSelect.dataset.value !== ''
            && theSelect.dataset.text !== '') {
            selectButtonInput.innerHTML = theSelect.dataset.text;
            uidInput.value = theSelect.dataset.value;
            textInput.value = theSelect.dataset.text;
        } else {
            if (theSelect.dataset.setFirstItem === theTrue) {
                setSingleSelect(theSelect);
            }
        }

        selectButton.onclick = function () { toggleSelectContent(this); };
        // show content when input
        selectButtonInput.onkeydown = function (e) {
            let theSelect = this.closest('.select');
            if (!theSelect.classList.contains('disabled')) {
                theSelect.classList.add('show');
                oldSearchTerm = this.innerHTML;
                // when click backspace with empty content
                if (e.keyCode === 13) {
                    document.execCommand('insertHTML', false, '');
                    // prevent the default behaviour of return key pressed
                    return false;
                }
            }
            else {
                e.preventDefault();
            }
        };
        // filter text
        selectButtonInput.onkeyup = function (e) {
            e.preventDefault();
            if (!theSelect.classList.contains('disabled')) {
                searchSelectContent(this, e);
            }
        };
    }

    function initSingleSelectInfo(parent, detailUrl) {
        // Create Info Btn
        let infoBtn = createElement('button');
        infoBtn.className = 'select-item-info-btn';
        infoBtn.innerHTML = 'i';
        parent.querySelector('.select-button').appendChild(infoBtn);

        // Create Info Popup
        let infoWrapper = createElement('div');
        infoWrapper.className = 'select-info-wrapper';
        let closePopover = createElement('button');
        closePopover.className = 'close-popover-btn';
        closePopover.innerHTML = 'x';
        infoWrapper.appendChild(closePopover);
        closePopover.onclick = function () { infoWrapper.classList.remove('show'); };
        let infoHeader = createElement('div');
        infoHeader.className = 'select-info-header';
        let infoImg = createElement('img');
        infoImg.className = 'select-info-img';
        let infoTitle = createElement('label');
        infoTitle.className = 'select-info-title';
        infoHeader.appendChild(infoImg);
        infoHeader.appendChild(infoTitle);
        let infoDesc = createElement('div');
        infoDesc.className = 'select-info-desc';
        infoWrapper.appendChild(infoHeader);
        infoWrapper.appendChild(infoDesc);
        parent.appendChild(infoWrapper);

        // Show popover when hover the info button
        infoBtn.onmouseover = function () {
            let text = parent.querySelector('.select-button-input').innerHTML.toLowerCase();
            if (text) {
                infoWrapper.classList.add('show');
                infoImg.setAttribute('src', 'url');
                infoTitle.innerHTML = 'select item text';
            }
        };
        // Show popup when click info button
        infoBtn.onclick = function (e) {
            e.preventDefault();
            e.stopPropagation();
            infoWrapper.classList.remove('show');

            //todo:get data from detailUrl

            let strHTML = '<div class="select-item-header">';
            strHTML += '<img class="select-item-image" src="' + 'Sample' + '">';
            strHTML += '<label class="select-item-text">' + 'sample text' + '</label>';
            strHTML += '</div>';
            showPopup('Detail', strHTML, true, function () { });
        };
    }

    function initSingleSelectContent(parent, json) {
        let selectContent = createElement('div');
        selectContent.className = 'select-content';
        if (json !== undefined) {
            for (let i = 0; i < json.length; i++) {
                let selectItem = initSelectItems(selectContent, json[i], parent.dataset.type, parent);
                if (selectItem !== null) {
                    selectItem.onclick = function () { onClickSingleSelectItem(this); };
                }
            }
        }

        parent.appendChild(selectContent);

        selectContent.onscroll = function () { addSelectContent(this); };
    }

    function confirmBeforeChange(theSelect, item, isSilent) {
        const
            popup = createDiv({
                'class': 'confirm-popup',
                'data-translate': 'are_you_sure_to_change_the_value',
            }, 'are_you_sure_to_change_the_value');

        if (!theSelect.querySelector('.confirm-popup')) {
            theSelect.appendChild(popup);
        }

        const
            buttonsWrap = createDiv({
                'class': 'confirm-popup-buttons',
            }),
            buttonDelete = createElement('button', {
                'class': 'red',
                'data-translate': 'change',
            }, 'change'),
            buttonCancel = createElement('button', {
                'class': 'green',
                'data-translate': 'cancel',
            }, 'cancel');

        buttonsWrap.appendChild(buttonDelete);
        buttonsWrap.appendChild(buttonCancel);
        popup.appendChild(buttonsWrap);

        buttonDelete.addEventListener('click', () => {
            popup.remove();
            onChangeSelect(theSelect, item, isSilent);
        });
        buttonCancel.addEventListener('click', () => {
            popup.remove();
        });
    }

    onChangeSelect = (theSelect, item, isSilent) => {
        // let theSelect = item.closest('.select');
        let theValue = item.getAttribute('value');
        let theText = item.innerHTML;

        if (theSelect.dataset.type === 'content') {
            theText = item.querySelector('.select-item-text').innerHTML;
            theSelect.querySelector('.select-button-input').innerHTML = theText;
        } else {
            theSelect.querySelector('.select-button-input').innerHTML = theText;
        }

        theSelect.querySelector('#' + theSelect.dataset.valueField).setAttribute('value', theValue);
        theSelect.querySelector('#' + theSelect.dataset.textField).setAttribute('value', theText);
        theSelect.dataset.value = theValue;
        theSelect.dataset.text = theText;

        reFillChildsData(theValue, theSelect.dataset.name);

        if (!isSilent) theSelect.dispatchEvent(theSelectChangeEvent);
        theSelect.classList.remove('show');
    }

    function onClickSingleSelectItem(item, isSilent) {
        let theSelect = item.closest('.select');

        let emptyCheck = '';
        if (theSelect.dataset.checkForEmptyElementBeforeChange) {
            const $ele = document.querySelectorAll(theSelect.dataset.checkForEmptyElementBeforeChange),
                prop = theSelect.dataset.emptyElementPropertyToCheck ? theSelect.dataset.emptyElementPropertyToCheck : 'textContent';
            if ($ele && $ele.length) {
                for (const ele of $ele) {
                    emptyCheck += ele[prop];
                }
            }
        }

        if (theSelect.dataset.confirmBeforeChange === "true" && theSelect.dataset.value !== '' && emptyCheck !== '') {
            confirmBeforeChange(theSelect, item, isSilent);
        } else {
            onChangeSelect(theSelect, item, isSilent);
        }
    }

    // Multiple Select Functions
    function initMultipleSelect(parent, json) {
        initMultipleSelectButton(parent);
        initMultipleSelectContent(parent, json);
    }

    function initMultipleSelectButton(theSelect) {
        let selectButton = createElement('div');
        selectButton.className = 'select-button';
        let selectButtonInput = createElement('div');
        selectButtonInput.className = 'select-button-input';
        selectButtonInput.setAttribute('contenteditable', theTrue);
        selectButton.appendChild(selectButtonInput);

        let uidInput = createElement('input');
        uidInput.setAttribute('type', 'hidden');
        uidInput.id = theSelect.dataset.valueField;
        uidInput.name = theSelect.dataset.valueField;
        uidInput.value = theSelect.dataset.value;

        let textInput = createElement('input');
        textInput.setAttribute('type', 'hidden');
        textInput.id = theSelect.dataset.textField;
        textInput.name = theSelect.dataset.textField;
        textInput.value = theSelect.dataset.text;

        //let clearSelectButton = createButton('x', clearMultipleSelect);
        //clearSelectButton.className = "clear-select-button";

        //selectButton.appendChild(clearSelectButton);

        selectButton.appendChild(uidInput);
        selectButton.appendChild(textInput);
        theSelect.appendChild(selectButton);

        if (theSelect.dataset.value !== '') {
            let values = theSelect.dataset.value.split(theValueSeparator);
            let names = theSelect.dataset.text.split(theValueSeparator);
            names.forEach(function (theText, index) {
                let activeItem = createElement('div');
                activeItem.className = 'active-item';
                activeItem.setAttribute('draggable', theTrue);
                activeItem.setAttribute('data-value', values[index]);
                let activeItemText = createElement('span');
                activeItemText.innerHTML = theText;
                activeItem.id = theSelect.dataset.valueField + '-' + activeItemText.innerHTML;

                let removeItem = createButton('X', removeMultipleItem);
                removeItem.className = 'remove-active-item';
                activeItem.appendChild(activeItemText);
                activeItem.appendChild(removeItem);
                selectButton.insertBefore(activeItem, selectButtonInput);
            });
        }

        selectButton.onclick = function () {
            toggleSelectContent(this);
        };

        selectButtonInput.onkeydown = function (e) {
            let theSelect = this.closest('.select');
            theSelect.classList.add('show');
            oldSearchTerm = this.innerHTML;
            // when click backspace with empty content
            if (e.keyCode === 13) {
                document.execCommand('insertHTML', false, '');
                // prevent the default behaviour of return key pressed
                return false;
            }
            if (!this.innerHTML && e.keyCode === 8) {
                let prev = this.previousSibling;
                if (prev && prev.className === 'active-item') {
                    let itemText = prev.querySelector('span').innerHTML;
                    prev.remove();
                    let type = theSelect.dataset.type;
                    let selectItems = theSelect.querySelectorAll('.select-item');
                    for (let i = 0; i < selectItems.length; i++) {
                        if (hasClass(selectItems[i], 'active')) {
                            if (type === 'content'
                                && selectItems[i].querySelector('.select-item-text').innerHTML === itemText) {
                                selectItems[i].classList.remove('active');
                            } else if (selectItems[i].innerHTML === itemText) {
                                selectItems[i].classList.remove('active');
                            }
                        }
                    }
                }
            }
        };
        selectButtonInput.onkeyup = function (e) {
            e.preventDefault();
            searchSelectContent(this, e);
        };
    }

    function initMultipleSelectContent(parent, json) {
        // Select content
        let type = parent.dataset.type;
        let selectContent = createElement('div');
        selectContent.className = 'select-content';
        for (let i = 0; i < json.length; i++) {
            let selectItem = initSelectItems(selectContent, json[i], type, parent);
            if (selectItem !== null) {
                selectItem.onmouseover = function () {
                    let selectItems = selectContent.querySelectorAll('.select-item');
                    for (let j = 0; j < selectItems.length; j++) {
                        if (hasClass(selectItems[j], 'hover')) {
                            selectItems[j].classList.remove('hover');
                        }
                    }
                    this.classList.add('hover');
                };
                selectItem.onclick = function () { onClickMultipleSelectItem(this); };
            }
        }
        parent.appendChild(selectContent);

        selectContent.onscroll = function () { addSelectContent(selectContent); };
    }

    function onClickMultipleSelectItem(item) {
        item.classList.add('active');

        let theSelect = item.closest('.select');
        let parent = theSelect.querySelector('.select-button');
        let selectInput = parent.querySelector('.select-button-input');
        let activeItem = createElement('div');
        activeItem.className = 'active-item';
        activeItem.setAttribute('draggable', theTrue);
        let activeItemText = createElement('span');
        if (theSelect.dataset.type === 'content') {
            activeItemText.innerHTML = item.querySelector('.select-item-text').innerHTML;
        } else {
            activeItemText.innerHTML = item.innerHTML;
        }
        activeItem.id = theSelect.dataset.valueField + '-' + activeItemText.innerHTML;
        activeItem.value = item.getAttribute('value');
        let removeItem = createButton('X', removeMultipleItem);
        removeItem.className = 'remove-active-item';
        activeItem.appendChild(activeItemText);
        activeItem.appendChild(removeItem);
        parent.insertBefore(activeItem, selectInput);

        let selectedItemValue = item.getAttribute('value');
        let selectedItemText = item.innerText;

        appendValueToSelect(theSelect, selectedItemValue, selectedItemText);

        //reposition the select content
        repositionSelectContent();
    }

    function repositionSelectContent() {
        const selectContentDOM = document.querySelector('.select-content');
        const selectDOM = document.querySelector('.custom-select');
        selectContentDOM.style.top = selectDOM.offsetHeight - 2 + "px";

        console.log('inside initSelectItems', selectContentDOM);
    }

    function appendValueToSelect(theSelect, selectedItemValue, selectedItemText) {
        let oldValue = getElement(theSelect.dataset.valueField).getAttribute('value');
        if (oldValue !== null && oldValue !== undefined && oldValue !== '') {
            selectedItemValue = oldValue + theValueSeparator + selectedItemValue;
        }

        let oldText = getElement(theSelect.dataset.textField).getAttribute('value');
        if (oldText !== null && oldText !== undefined && oldText !== '') {
            selectedItemText = oldText + theValueSeparator + selectedItemText;
        }

        getElement(theSelect.dataset.valueField).setAttribute('value', selectedItemValue);
        getElement(theSelect.dataset.textField).setAttribute('value', selectedItemText);
    }

    function removeMultipleItem(e) {
        e.preventDefault();
        e.stopPropagation();
        let theSelect = this.closest('.select');
        let activeItem = this.closest('.active-item');

        let itemText = activeItem.querySelector('span').innerHTML;
        let itemValue = activeItem.value;

        let texts = getElement(theSelect.dataset.textField).getAttribute('value');
        let values = getElement(theSelect.dataset.valueField).getAttribute('value');

        getElement(theSelect.dataset.valueField).setAttribute('value', values.replace(itemValue, ''));
        getElement(theSelect.dataset.textField).setAttribute('value', texts.replace(itemText, ''));

        activeItem.remove();
        let selectItems = theSelect.querySelectorAll('.select-item');
        for (let i = 0; i < selectItems.length; i++) {
            if (hasClass(selectItems[i], 'active')) {
                if (theSelect.dataset.type === 'content'
                    && selectItems[i].querySelector('.select-item-text').innerHTML === itemText) {
                    selectItems[i].classList.remove('active');
                } else if (selectItems[i].innerHTML === itemText) {
                    selectItems[i].classList.remove('active');
                }
            }
        }

        //reposition selectContent
        repositionSelectContent();
    }
    
    function clearContent(theSelect) {
        let selectContent = theSelect.querySelector('.select-content');
        selectContent.innerHTML = '';
    }

    function onClearButtonClick(e) {
        e.preventDefault();
        e.stopPropagation();

        const theSelect = e.target.closest('.select');
        if (!theSelect.classList.contains("disabled"))
            clearSelect(theSelect);
    }

    function confirmBeforeClear(theSelect) {
        const
            popup = createDiv({
                'class': 'confirm-popup',
                'data-translate': 'are_you_sure_to_clear_this_field',
            }, 'are_you_sure_to_clear_this_field');

        if (!theSelect.querySelector('.confirm-popup')) {
            theSelect.appendChild(popup);
        }

        const
            buttonsWrap = createDiv({
                'class': 'confirm-popup-buttons',
            }),
            buttonDelete = createElement('button', {
                'class': 'red',
                'data-translate': 'delete',
            }, 'clear'),
            buttonCancel = createElement('button', {
                'class': 'green',
                'data-translate': 'cancel',
            }, 'cancel');

        buttonsWrap.appendChild(buttonDelete);
        buttonsWrap.appendChild(buttonCancel);
        popup.appendChild(buttonsWrap);

        buttonDelete.addEventListener('click', () => {
            popup.remove();
            onClearSelect(theSelect);
        });
        buttonCancel.addEventListener('click', () => {
            popup.remove();
        });
    }

    clearSelect = (theSelect) => {
        if (theSelect.dataset.confirmBeforeClear === "true" && theSelect.dataset.value !== '') {
            confirmBeforeClear(theSelect);
        } else {
            onClearSelect(theSelect);
        }
    }

    onClearSelect = (theSelect) => {
        let selectButton = theSelect.querySelector('.select-button');
        if (!selectButton) return;
        let selectButtonInput = selectButton.querySelector('.select-button-input');
        selectButtonInput.innerHTML = '';
        theSelect.dataset.value = '';
        theSelect.dataset.text = '';
        getElement(theSelect.dataset.valueField).value = '';
        getElement(theSelect.dataset.textField).value = '';

        // Refills the data
        let parentId;
        let parentName = theSelect.dataset.valueField;
        allSelects.forEach(function (s) {
            if (s.dataset.valueField === theSelect.dataset.parent) {
                parentId = getElement(theSelect.dataset.valueField).getAttribute('value');
            }
            // If parent cleared, clear child select also
            if (s.dataset.parent === parentName) {
                if (hasClass(s, 'radio-select')) {
                    s.querySelector('.select-radio-items').innerHTML = ' ';
                } else {
                    let sSbtn = s.querySelector('.select-button');
                    if (sSbtn) {
                        s.querySelector('.select-button-input').innerHTML = '';
                        s.querySelector('.select-content').innerHTML = '';
                        s.dataset.value = '';
                        s.dataset.text = '';
                    }
                }
            }
        });

        if (theSelect.classList.contains('disabled')) {
            return;
        }

        let url = theSelect.dataset.url + '?q=' + '&take=' + loadCount;
        if (parentId !== undefined) {
            url += '&parent=' + parentId;
        }

        if (!urlArray.includes(url)) {
            doGet(url, function (req) {
                let json = JSON.parse(req.responseText);
                updateSelectContent(theSelect, json);
            });
        }

        theSelect.dispatchEvent(theSelectClearEvent);
    }

    function clearMultipleSelect(e) {
        e.preventDefault();
        e.stopPropagation();

        let theSelect = this.closest('.select');

        let uidInput = theSelect.querySelector(`#${theSelect.dataset.valueField}`);
        let nameInput = theSelect.querySelector(`#${theSelect.dataset.textField}`);
        if (uidInput) {
            uidInput.value = '';
        }
        if (nameInput) {
            nameInput.value = '';
        }

        let items = theSelect.querySelectorAll('.remove-active-item');

        items.forEach(function (val) {
            val.click();
        });
    }

    clearMultipleSelectContent = (theSelect) => {
        let uidInput = theSelect.querySelector(`#${theSelect.dataset.valueField}`);
        let nameInput = theSelect.querySelector(`#${theSelect.dataset.textField}`);
        if (uidInput) {
            uidInput.value = '';
        }
        if (nameInput) {
            nameInput.value = '';
        }

        let items = theSelect.querySelectorAll('.remove-active-item');

        items.forEach(function (val) {
            val.click();
        });
    }

    document.addEventListener("click",

        function (e) {
            let activeSelects = document.querySelectorAll('.select.show');
            activeSelects.forEach(s => {
                if (s !== getParent(e.target, 'select')) {
                    s.classList.remove('show');
                }
            });

            let activePopover = document.querySelector('.select-info-wrapper.show');
            if (activePopover && activePopover.contains(e.target) === false) {
                activePopover.classList.remove('show');
            }
        });

    document.addEventListener('keyup',
        function (e) {
            allSelects = document.querySelectorAll('div.select');
            let activeSelect;
            for (let i = 0; i < allSelects.length; i++) {
                if (hasClass(allSelects[i], 'show')) {
                    activeSelect = allSelects[i];
                }
            }
            let activeSelectContent, activeSelectItems;
            let currentSelectItemId = -1;
            if (activeSelect) {
                activeSelectContent = activeSelect.querySelector('.select-content');
                activeSelectItems = activeSelectContent.querySelectorAll('.select-item');
                for (let i = 0; i < activeSelectItems.length; i++) {
                    if (hasClass(activeSelectItems[i], 'hover')) { currentSelectItemId = i; }
                }
                // Up arrow key
                if (e.keyCode === 38) {
                    e.preventDefault();
                    if (currentSelectItemId - 1 >= 0) {
                        activeSelectContent.scrollTop -= activeSelectItems[currentSelectItemId].offsetHeight;
                        activeSelectItems[currentSelectItemId].classList.remove('hover');
                        activeSelectItems[currentSelectItemId - 1].classList.add('hover');
                    }
                }
                // Down arrow key
                if (e.keyCode === 40) {
                    e.preventDefault();
                    if (currentSelectItemId + 1 < activeSelectItems.length) {
                        if (currentSelectItemId === -1) {
                            activeSelectItems[currentSelectItemId + 1].classList.add('hover');
                        } else {
                            activeSelectContent.scrollTop += activeSelectItems[currentSelectItemId].offsetHeight;
                            activeSelectItems[currentSelectItemId].classList.remove('hover');
                            activeSelectItems[currentSelectItemId + 1].classList.add('hover');
                        }
                    }
                }
                // Press enter key
                if (e.keyCode === 13
                    && activeSelectItems[currentSelectItemId]
                    && hasClass(activeSelectItems[currentSelectItemId], 'active') === false
                    && isNotChanged === false) {
                    if (hasClass(activeSelect, 'single-select')) { onClickSingleSelectItem(activeSelectItems[currentSelectItemId]); } else if (hasClass(activeSelect, 'multiple-select')) { onClickMultipleSelectItem(activeSelectItems[currentSelectItemId]); }
                }
            }
        },
        false);

    const getSelectData = (select, url, cb) => {
        if (selectDataCache[url]) {
            setTimeout(() => {
                cb(selectDataCache[url]);
            }, 4);
        } else if (select.dataset.json) {
            cb(selectDataCache[select.dataset.json]);
        } else {
            doGet(url, (res) => {
                selectDataCache[url] = res;
                cb(res);
            });
        }
    };

    initSelects = function () {
        document.querySelectorAll('div.select').forEach(function (s) {
            refreshSelect(s, () => {
                s.dataset.is_init_done = 'true';
                s.dispatchEvent(theSelectInitEvent);
            })
        });
    };

    initSelectContentFromJson = function (theSelect, json) {
        const jsonDataUid = `data-json-${guid()}`;
        theSelect.dataset.url = 'data-json';
        theSelect.dataset.json = jsonDataUid;
        window.selectDataCache[jsonDataUid] = {
            responseText: JSON.stringify(json),
        };
        refreshSelect(theSelect);
    };

    resetSelectWithoutCloning = function (theSelect) {
        theSelect.dataset.value = '';
        theSelect.dataset.text = '';
        clearChildren(theSelect);
        return theSelect;
    };


    //reposition selectContent on resize
    window.addEventListener("resize", repositionSelectContent);

    initSelects();
})();
