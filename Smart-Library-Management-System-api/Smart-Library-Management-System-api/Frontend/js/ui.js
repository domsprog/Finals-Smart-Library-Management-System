
// Show loading spinner
function showLoading() {
    const spinner = document.getElementById('loading');
    if (spinner) {
        spinner.classList.add('show');
    }
}

// Hide loading spinner
function hideLoading() {
    const spinner = document.getElementById('loading');
    if (spinner) {
        spinner.classList.remove('show');
    }
}

// Show toast notification
function showToast(message, type = 'success') {
    const toast = document.getElementById('toast');
    if (!toast) return;
    
    // Set message and type
    toast.textContent = message;
    toast.className = `toast ${type}`;
    
    // Show toast
    toast.classList.add('show');
    
    // Hide after duration
    setTimeout(() => {
        toast.classList.remove('show');
    }, APP_CONFIG.toastDuration);
}

// Toggle sidebar (for mobile)
function toggleSidebar() {
    const sidebar = document.getElementById('sidebar');
    if (sidebar) {
        sidebar.classList.toggle('active');
    }
}

// Format date
function formatDate(dateString) {
    if (!dateString) return 'N/A';
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', {
        year: 'numeric',
        month: 'short',
        day: 'numeric'
    });
}

// Format currency
function formatCurrency(amount) {
    return `₱${parseFloat(amount).toFixed(2)}`;
}

// Create modal
function createModal(title, content, buttons = []) {
    // Create modal HTML
    const modalHTML = `
        <div class="modal" id="customModal">
            <div class="modal-content">
                <div class="modal-header">
                    <h2>${title}</h2>
                    <span class="modal-close" onclick="closeModal()">×</span>
                </div>
                <div class="modal-body">
                    ${content}
                </div>
                <div class="modal-footer" style="margin-top: 20px; text-align: right;">
                    ${buttons.map(btn => `
                        <button class="btn ${btn.class}" onclick="${btn.onclick}">
                            ${btn.text}
                        </button>
                    `).join('')}
                </div>
            </div>
        </div>
    `;
    
    // Remove existing modal
    const existingModal = document.getElementById('customModal');
    if (existingModal) {
        existingModal.remove();
    }
    
    // Add modal to body
    document.body.insertAdjacentHTML('beforeend', modalHTML);
    
    // Show modal
    setTimeout(() => {
        document.getElementById('customModal').classList.add('show');
    }, 10);
}

// Close modal
function closeModal() {
    const modal = document.getElementById('customModal');
    if (modal) {
        modal.classList.remove('show');
        setTimeout(() => modal.remove(), 300);
    }
}

// Create table from data
function createTable(data, columns) {
    if (!data || data.length === 0) {
        return '<p style="text-align: center; padding: 40px; color: #999;">No data available</p>';
    }
    
    let html = '<table><thead><tr>';
    
    // Create table headers
    columns.forEach(col => {
        html += `<th>${col.label}</th>`;
    });
    html += '</tr></thead><tbody>';
    
    // Create table rows
    data.forEach(row => {
        html += '<tr>';
        columns.forEach(col => {
            let value = row[col.field];
            
            // Apply formatter if exists
            if (col.formatter) {
                value = col.formatter(value, row);
            }
            
            html += `<td>${value || 'N/A'}</td>`;
        });
        html += '</tr>';
    });
    
    html += '</tbody></table>';
    return html;
}

// Create form
function createForm(fields, submitHandler) {
    let html = '<form id="dynamicForm">';
    
    fields.forEach(field => {
        html += `
            <div class="form-group">
                <label for="${field.name}">${field.label} ${field.required ? '*' : ''}</label>
        `;
        
        if (field.type === 'textarea') {
            html += `<textarea id="${field.name}" name="${field.name}" 
                     ${field.required ? 'required' : ''}></textarea>`;
        } else if (field.type === 'select') {
            html += `<select id="${field.name}" name="${field.name}" 
                     ${field.required ? 'required' : ''}>
                     <option value="">Select ${field.label}</option>`;
            field.options.forEach(opt => {
                html += `<option value="${opt.value}">${opt.label}</option>`;
            });
            html += '</select>';
        } else {
            html += `<input type="${field.type || 'text'}" 
                     id="${field.name}" 
                     name="${field.name}" 
                     placeholder="${field.placeholder || ''}"
                     ${field.required ? 'required' : ''}>`;
        }
        
        html += '</div>';
    });
    
    html += '</form>';
    return html;
}

// Get form data
function getFormData(formId) {
    const form = document.getElementById(formId);
    if (!form) return null;
    
    const formData = new FormData(form);
    const data = {};
    
    for (let [key, value] of formData.entries()) {
        data[key] = value;
    }
    
    return data;
}

// Confirm dialog
function confirmDialog(message, onConfirm) {
    const content = `<p style="font-size: 16px; margin: 20px 0;">${message}</p>`;
    const buttons = [
        {
            text: 'Cancel',
            class: 'btn-secondary',
            onclick: 'closeModal()'
        },
        {
            text: 'Confirm',
            class: 'btn-danger',
            onclick: `(${onConfirm.toString()})(); closeModal();`
        }
    ];
    
    createModal('Confirm Action', content, buttons);
}

// Debounce function for search
function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}