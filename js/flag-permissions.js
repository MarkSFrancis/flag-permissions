var permissionValues = {
    read: bigInt(2).pow(0),
    write: bigInt(2).pow(1),
    createDelete: bigInt(2).pow(2),
    audit: bigInt(2).pow(3)
};

var elements = {
    inputs: {
        wrapper: document.getElementById('js-permission-input'),
        read: document.getElementById('js-read'),
        write: document.getElementById('js-write'),
        createDelete: document.getElementById('js-create-delete'),
        audit: document.getElementById('js-audit')
    },
    outputs: {
        value: document.getElementById('js-permission-value-output'),
        text: document.getElementById('js-permission-text-output')
    }
}

var myPermissionsValue = bigInt(0);

function hasPermission(permissionValue) {
    return myPermissionsValue.and(permissionValue).equals(permissionValue);
}

function togglePermission(permissionValue, shouldGrant) {
    if (shouldGrant) {
        myPermissionsValue = myPermissionsValue.or(permissionValue);
    } else if (hasPermission(permissionValue)) {
        myPermissionsValue = myPermissionsValue.subtract(permissionValue);
    }

    updateOutput();
}

function updateOutput() {
    elements.outputs.value.innerText = myPermissionsValue.toString();

    var permissionsSummary = [];
    if (hasPermission(permissionValues.read)) {
        permissionsSummary.push('Read');
    }
    if (hasPermission(permissionValues.write)) {
        permissionsSummary.push('Write');
    }
    if (hasPermission(permissionValues.createDelete)) {
        permissionsSummary.push('Create/ Delete');
    }
    if (hasPermission(permissionValues.audit)) {
        permissionsSummary.push('Audit');
    }

    elements.outputs.text.innerText = permissionsSummary.join(', ');
}

elements.inputs.wrapper.onchange = function (ev) {
    if (ev.target === elements.inputs.read) {
        togglePermission(permissionValues.read, ev.target.checked);
    } else if (ev.target === elements.inputs.write) {
        togglePermission(permissionValues.write, ev.target.checked);
    } else if (ev.target === elements.inputs.createDelete) {
        togglePermission(permissionValues.createDelete, ev.target.checked);
    } else if (ev.target === elements.inputs.audit) {
        togglePermission(permissionValues.audit, ev.target.checked);
    } else {
        console.error('Unknown permission type checkbox', ev);
    }
};