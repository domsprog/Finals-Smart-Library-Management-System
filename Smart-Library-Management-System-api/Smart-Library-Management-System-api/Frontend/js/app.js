// Current page state
let currentPage = 'dashboard';

// Initialize app when DOM is loaded
document.addEventListener('DOMContentLoaded', function() {
    console.log('🚀 Smart Library System Started');
    
    // Setup navigation
    setupNavigation();
    
    // Load dashboard by default
    loadPage('dashboard');
    
    // Setup mobile sidebar
    setupMobileSidebar();
});

// Setup navigation menu
function setupNavigation() {
    const menuItems = document.querySelectorAll('.menu-item');
    
    menuItems.forEach(item => {
        item.addEventListener('click', function(e) {
            e.preventDefault();
            
            // Remove active class from all items
            menuItems.forEach(i => i.classList.remove('active'));
            
            // Add active class to clicked item
            this.classList.add('active');
            
            // Get page name
            const pageName = this.getAttribute('data-page');
            
            // Load page
            loadPage(pageName);
            
            // Close sidebar on mobile
            if (window.innerWidth <= 768) {
                toggleSidebar();
            }
        });
    });
}

// Load different pages
function loadPage(pageName) {
    currentPage = pageName;
    const container = document.getElementById('content-container');
    
    // Update page title
    updatePageTitle(pageName);
    
    // Load page content based on page name
    switch(pageName) {
        case 'dashboard':
            loadDashboard();
            break;
        case 'books':
            loadBooksPage();
            break;
        case 'users':
            loadUsersPage();
            break;
        case 'loans':
            loadLoansPage();
            break;
        case 'fines':
            loadFinesPage();
            break;
        case 'reservations':
            loadReservationsPage();
            break;
        default:
            container.innerHTML = '<p>Page not found</p>';
    }
}

// Update page title
function updatePageTitle(pageName) {
    const titleElement = document.getElementById('page-title');
    const subtitleElement = document.getElementById('page-subtitle');
    
    const titles = {
        dashboard: {
            title: 'Welcome to Smart Library System',
            subtitle: 'Manage your library efficiently'
        },
        books: {
            title: 'Books Management',
            subtitle: 'Add, edit, and manage library books'
        },
        users: {
            title: 'Users Management',
            subtitle: 'Manage students and faculty members'
        },
        loans: {
            title: 'Loans Management',
            subtitle: 'Track borrowed and returned books'
        },
        fines: {
            title: 'Fines Management',
            subtitle: 'Manage overdue fines and payments'
        },
        reservations: {
            title: 'Reservations Management',
            subtitle: 'Handle book reservations'
        }
    };
    
    titleElement.textContent = titles[pageName].title;
    subtitleElement.textContent = titles[pageName].subtitle;
}

// ========== DASHBOARD ==========
async function loadDashboard() {
    const container = document.getElementById('content-container');
    
    // Show loading
    container.innerHTML = '<p>Loading dashboard...</p>';
    
    try {
        // Fetch data from all endpoints
        const [booksResult, usersResult, loansResult, finesResult] = await Promise.all([
            BooksAPI.getAll(),
            UsersAPI.getAll(),
            LoansAPI.getAll(),
            FinesAPI.getAll()
        ]);
        
        // Calculate statistics
        const totalBooks = booksResult.success ? booksResult.data.length : 0;
        const totalUsers = usersResult.success ? usersResult.data.length : 0;
        const activeLoans = loansResult.success ? 
            loansResult.data.filter(l => !l.isReturned).length : 0;
        const totalFines = finesResult.success ? 
            finesResult.data.reduce((sum, f) => sum + (f.isPaid ? 0 : f.amount), 0) : 0;
        
        // Create dashboard HTML
        container.innerHTML = `
            <div class="cards-grid">
                <div class="card" onclick="loadPage('books')">
                    <div class="card-icon">📚</div>
                    <h3>Total Books</h3>
                    <p style="font-size: 32px; font-weight: bold; color: var(--db-blue); margin-top: 10px;">
                        ${totalBooks}
                    </p>
                    <p>books in the library</p>
                </div>
                
                <div class="card" onclick="loadPage('users')">
                    <div class="card-icon">👥</div>
                    <h3>Active Users</h3>
                    <p style="font-size: 32px; font-weight: bold; color: var(--db-blue); margin-top: 10px;">
                        ${totalUsers}
                    </p>
                    <p>registered users</p>
                </div>
                
                <div class="card" onclick="loadPage('loans')">
                    <div class="card-icon">📖</div>
                    <h3>Active Loans</h3>
                    <p style="font-size: 32px; font-weight: bold; color: var(--db-blue); margin-top: 10px;">
                        ${activeLoans}
                    </p>
                    <p>books currently borrowed</p>
                </div>
                
                <div class="card" onclick="loadPage('fines')">
                    <div class="card-icon">💰</div>
                    <h3>Pending Fines</h3>
                    <p style="font-size: 32px; font-weight: bold; color: var(--db-blue); margin-top: 10px;">
                        ${formatCurrency(totalFines)}
                    </p>
                    <p>in unpaid fines</p>
                </div>
            </div>
            
            <div style="margin-top: 30px;">
                <h2 style="margin-bottom: 20px;">Quick Actions</h2>
                <div class="action-buttons">
                    <button class="btn btn-primary" onclick="showAddBookForm()">
                        📚 Add New Book
                    </button>
                    <button class="btn btn-primary" onclick="showAddUserForm()">
                        👤 Register User
                    </button>
                    <button class="btn btn-primary" onclick="showBorrowBookForm()">
                        📖 Process Loan
                    </button>
                    <button class="btn btn-success" onclick="loadPage('reservations')">
                        📅 View Reservations
                    </button>
                </div>
            </div>
        `;
        
    } catch (error) {
        container.innerHTML = `<p style="color: red;">Error loading dashboard: ${error.message}</p>`;
    }
}

// ========== BOOKS PAGE ==========
async function loadBooksPage() {
    const container = document.getElementById('content-container');
    container.innerHTML = '<p>Loading books...</p>';
    
    const result = await BooksAPI.getAll();
    
    if (!result.success) {
        showToast('Error loading books: ' + result.error, 'error');
        container.innerHTML = '<p style="color: red;">Error loading books</p>';
        return;
    }
    
    const columns = [
        { label: 'ISBN', field: 'isbn' },
        { label: 'Title', field: 'title' },
        { label: 'Author', field: 'author' },
        { label: 'Category', field: 'category' },
        { label: 'Available', field: 'availableCopies' },
        { label: 'Total', field: 'totalCopies' },
        { label: 'Price', field: 'price', formatter: (val) => formatCurrency(val) },
        { 
            label: 'Actions', 
            field: 'isbn',
            formatter: (val) => `
                <button class="btn btn-warning" onclick="editBook('${val}')">Edit</button>
                <button class="btn btn-danger" onclick="deleteBook('${val}')">Delete</button>
            `
        }
    ];
    
    container.innerHTML = `
        <div style="margin-bottom: 20px;">
            <button class="btn btn-primary" onclick="showAddBookForm()">➕ Add New Book</button>
            <input type="text" 
                   id="searchBooks" 
                   placeholder="Search books..." 
                   style="padding: 12px; width: 300px; margin-left: 10px; border: 2px solid #ddd; border-radius: 8px;"
                   onkeyup="searchBooks(this.value)">
        </div>
        <div class="table-container">
            ${createTable(result.data, columns)}
        </div>
    `;
}

// Add book form
function showAddBookForm() {
    const fields = [
        { name: 'isbn', label: 'ISBN', type: 'text', required: true },
        { name: 'title', label: 'Title', type: 'text', required: true },
        { name: 'author', label: 'Author', type: 'text', required: true },
        { name: 'publisher', label: 'Publisher', type: 'text' },
        { name: 'publicationYear', label: 'Publication Year', type: 'number' },
        { name: 'category', label: 'Category', type: 'text' },
        { name: 'price', label: 'Price', type: 'number', required: true },
        { name: 'totalCopies', label: 'Total Copies', type: 'number', required: true },
        { name: 'availableCopies', label: 'Available Copies', type: 'number', required: true }
    ];
    
    const formHTML = createForm(fields);
    
    const buttons = [
        { text: 'Cancel', class: 'btn-secondary', onclick: 'closeModal()' },
        { text: 'Add Book', class: 'btn-primary', onclick: 'submitAddBook()' }
    ];
    
    createModal('Add New Book', formHTML, buttons);
}

// Submit add book
async function submitAddBook() {
    const data = getFormData('dynamicForm');
    
    if (!data) {
        showToast('Please fill all required fields', 'error');
        return;
    }
    
    const result = await BooksAPI.create(data);
    
    if (result.success) {
        showToast('Book added successfully!', 'success');
        closeModal();
        loadBooksPage();
    } else {
        showToast('Error adding book: ' + result.error, 'error');
    }
}

// Delete book
function deleteBook(isbn) {
    confirmDialog('Are you sure you want to delete this book?', async () => {
        const result = await BooksAPI.delete(isbn);
        
        if (result.success) {
            showToast('Book deleted successfully!', 'success');
            loadBooksPage();
        } else {
            showToast('Error deleting book: ' + result.error, 'error');
        }
    });
}

// ========== USERS PAGE ==========
async function loadUsersPage() {
    const container = document.getElementById('content-container');
    container.innerHTML = '<p>Loading users...</p>';
    
    const result = await UsersAPI.getAll();
    
    if (!result.success) {
        showToast('Error loading users: ' + result.error, 'error');
        return;
    }
    
    const columns = [
        { label: 'User ID', field: 'userId' },
        { label: 'Name', field: 'name' },
        { label: 'Email', field: 'email' },
        { label: 'Type', field: 'userType' },
        { label: 'Department', field: 'department' },
        { 
            label: 'Registered', 
            field: 'registeredDate',
            formatter: (val) => formatDate(val)
        },
        { 
            label: 'Actions', 
            field: 'userId',
            formatter: (val) => `
                <button class="btn btn-danger" onclick="deleteUser('${val}')">Delete</button>
            `
        }
    ];
    
    container.innerHTML = `
        <div style="margin-bottom: 20px;">
            <button class="btn btn-primary" onclick="showAddUserForm()">➕ Add New User</button>
        </div>
        <div class="table-container">
            ${createTable(result.data, columns)}
        </div>
    `;
}

// Show add user form
function showAddUserForm() {
    const content = `
        <div class="form-group">
            <label>User Type</label>
            <select id="userType" onchange="updateUserForm()">
                <option value="">Select Type</option>
                <option value="student">Student</option>
                <option value="faculty">Faculty</option>
            </select>
        </div>
        <div id="userFormFields"></div>
    `;
    
    const buttons = [
        { text: 'Cancel', class: 'btn-secondary', onclick: 'closeModal()' },
        { text: 'Register User', class: 'btn-primary', onclick: 'submitAddUser()' }
    ];
    
    createModal('Register New User', content, buttons);
}

// Update user form based on type
function updateUserForm() {
    const userType = document.getElementById('userType').value;
    const fieldsContainer = document.getElementById('userFormFields');
    
    if (!userType) {
        fieldsContainer.innerHTML = '';
        return;
    }
    
    let fields = [
        { name: 'userId', label: 'User ID', type: 'text', required: true },
        { name: 'name', label: 'Name', type: 'text', required: true },
        { name: 'email', label: 'Email', type: 'email', required: true },
        { name: 'department', label: 'Department', type: 'text', required: true }
    ];
    
    if (userType === 'student') {
        fields.push({ name: 'studentId', label: 'Student ID', type: 'text', required: true });
    } else {
        fields.push(
            { name: 'facultyId', label: 'Faculty ID', type: 'text', required: true },
            { name: 'position', label: 'Position', type: 'text', required: true }
        );
    }
    
    fieldsContainer.innerHTML = createForm(fields);
}

// Submit add user
async function submitAddUser() {
    const userType = document.getElementById('userType').value;
    
    if (!userType) {
        showToast('Please select user type', 'error');
        return;
    }
    
    const data = getFormData('dynamicForm');
    
    if (!data) {
        showToast('Please fill all required fields', 'error');
        return;
    }
    
    let result;
    if (userType === 'student') {
        result = await UsersAPI.registerStudent(data);
    } else {
        result = await UsersAPI.registerFaculty(data);
    }
    
    if (result.success) {
        showToast('User registered successfully!', 'success');
        closeModal();
        loadUsersPage();
    } else {
        showToast('Error registering user: ' + result.error, 'error');
    }
}

// Delete user
function deleteUser(userId) {
    confirmDialog('Are you sure you want to delete this user?', async () => {
        const result = await UsersAPI.delete(userId);
        
        if (result.success) {
            showToast('User deleted successfully!', 'success');
            loadUsersPage();
        } else {
            showToast('Error deleting user: ' + result.error, 'error');
        }
    });
}

// ========== LOANS PAGE ==========
async function loadLoansPage() {
    const container = document.getElementById('content-container');
    container.innerHTML = '<p>Loading loans...</p>';
    
    const result = await LoansAPI.getAll();
    
    if (!result.success) {
        showToast('Error loading loans: ' + result.error, 'error');
        return;
    }
    
    const columns = [
        { label: 'Loan ID', field: 'loanId' },
        { label: 'User ID', field: 'userId' },
        { label: 'ISBN', field: 'isbn' },
        { label: 'Borrow Date', field: 'borrowDate', formatter: (val) => formatDate(val) },
        { label: 'Due Date', field: 'dueDate', formatter: (val) => formatDate(val) },
        { label: 'Returned', field: 'isReturned', formatter: (val) => val ? '✅ Yes' : '❌ No' },
        { label: 'Fine', field: 'fineAmount', formatter: (val) => formatCurrency(val) },
        { 
            label: 'Actions', 
            field: 'loanId',
            formatter: (val, row) => !row.isReturned ? 
                `<button class="btn btn-success" onclick="returnBook('${val}')">Return</button>` : 
                'Returned'
        }
    ];
    
    container.innerHTML = `
        <div style="margin-bottom: 20px;">
            <button class="btn btn-primary" onclick="showBorrowBookForm()">➕ New Loan</button>
        </div>
        <div class="table-container">
            ${createTable(result.data, columns)}
        </div>
    `;
}

// Show borrow book form
function showBorrowBookForm() {
    const fields = [
        { name: 'userId', label: 'User ID', type: 'text', required: true },
        { name: 'isbn', label: 'Book ISBN', type: 'text', required: true }
    ];
    
    const formHTML = createForm(fields);
    
    const buttons = [
        { text: 'Cancel', class: 'btn-secondary', onclick: 'closeModal()' },
        { text: 'Borrow Book', class: 'btn-primary', onclick: 'submitBorrowBook()' }
    ];
    
    createModal('Borrow Book', formHTML, buttons);
}

// Submit borrow book
async function submitBorrowBook() {
    const data = getFormData('dynamicForm');
    
    if (!data) {
        showToast('Please fill all required fields', 'error');
        return;
    }
    
    const result = await LoansAPI.borrow(data);
    
    if (result.success) {
        showToast('Book borrowed successfully!', 'success');
        closeModal();
        loadLoansPage();
    } else {
        showToast('Error borrowing book: ' + result.error, 'error');
    }
}

// Return book
function returnBook(loanId) {
    confirmDialog('Mark this book as returned?', async () => {
        const result = await LoansAPI.return({ loanId });
        
        if (result.success) {
            showToast('Book returned successfully!', 'success');
            loadLoansPage();
        } else {
            showToast('Error returning book: ' + result.error, 'error');
        }
    });
}

// ========== FINES PAGE ==========
async function loadFinesPage() {
    const container = document.getElementById('content-container');
    container.innerHTML = '<p>Loading fines...</p>';
    
    const result = await FinesAPI.getAll();
    
    if (!result.success) {
        showToast('Error loading fines: ' + result.error, 'error');
        return;
    }
    
    const columns = [
        { label: 'Fine ID', field: 'fineId' },
        { label: 'User ID', field: 'userId' },
        { label: 'Loan ID', field: 'loanId' },
        { label: 'Amount', field: 'amount', formatter: (val) => formatCurrency(val) },
        { label: 'Reason', field: 'reason' },
        { label: 'Paid', field: 'isPaid', formatter: (val) => val ? '✅ Yes' : '❌ No' },
        { label: 'Created', field: 'createdDate', formatter: (val) => formatDate(val) },
        { 
            label: 'Actions', 
            field: 'fineId',
            formatter: (val, row) => !row.isPaid ? 
                `<button class="btn btn-success" onclick="payFine('${val}')">Pay Fine</button>` : 
                'Paid'
        }
    ];
    
    container.innerHTML = `
        <div class="table-container">
            ${createTable(result.data, columns)}
        </div>
    `;
}

// Pay fine
function payFine(fineId) {
    confirmDialog('Mark this fine as paid?', async () => {
        const result = await FinesAPI.pay(fineId);
        
        if (result.success) {
            showToast('Fine paid successfully!', 'success');
            loadFinesPage();
        } else {
            showToast('Error paying fine: ' + result.error, 'error');
        }
    });
}

// ========== RESERVATIONS PAGE ==========
async function loadReservationsPage() {
    const container = document.getElementById('content-container');
    container.innerHTML = '<p>Loading reservations...</p>';
    
    const result = await ReservationsAPI.getAll();
    
    if (!result.success) {
        showToast('Error loading reservations: ' + result.error, 'error');
        return;
    }
    
    const columns = [
        { label: 'ID', field: 'reservationId' },
        { label: 'User ID', field: 'userId' },
        { label: 'ISBN', field: 'isbn' },
        { label: 'Reserved', field: 'reservationDate', formatter: (val) => formatDate(val) },
        { label: 'Expires', field: 'expiryDate', formatter: (val) => formatDate(val) },
        { label: 'Active', field: 'isActive', formatter: (val) => val ? '✅ Yes' : '❌ No' },
        { label: 'Fulfilled', field: 'isFulfilled', formatter: (val) => val ? '✅ Yes' : '❌ No' },
        { 
            label: 'Actions', 
            field: 'reservationId',
            formatter: (val, row) => row.isActive && !row.isFulfilled ? 
                `<button class="btn btn-success" onclick="fulfillReservation('${val}')">Fulfill</button>
                 <button class="btn btn-danger" onclick="cancelReservation('${val}')">Cancel</button>` : 
                'Completed'
        }
    ];
    
    container.innerHTML = `
        <div style="margin-bottom: 20px;">
            <button class="btn btn-primary" onclick="showAddReservationForm()">➕ New Reservation</button>
        </div>
        <div class="table-container">
            ${createTable(result.data, columns)}
        </div>
    `;
}

// Show add reservation form
function showAddReservationForm() {
    const fields = [
        { name: 'userId', label: 'User ID', type: 'text', required: true },
        { name: 'isbn', label: 'Book ISBN', type: 'text', required: true }
    ];
    
    const formHTML = createForm(fields);
    
    const buttons = [
        { text: 'Cancel', class: 'btn-secondary', onclick: 'closeModal()' },
        { text: 'Create Reservation', class: 'btn-primary', onclick: 'submitAddReservation()' }
    ];
    
    createModal('New Reservation', formHTML, buttons);
}

// Submit add reservation
async function submitAddReservation() {
    const data = getFormData('dynamicForm');
    
    if (!data) {
        showToast('Please fill all required fields', 'error');
        return;
    }
    
    const result = await ReservationsAPI.create(data);
    
    if (result.success) {
        showToast('Reservation created successfully!', 'success');
        closeModal();
        loadReservationsPage();
    } else {
        showToast('Error creating reservation: ' + result.error, 'error');
    }
}

// Fulfill reservation
function fulfillReservation(reservationId) {
    confirmDialog('Fulfill this reservation?', async () => {
        const result = await ReservationsAPI.fulfill(reservationId);
        
        if (result.success) {
            showToast('Reservation fulfilled successfully!', 'success');
            loadReservationsPage();
        } else {
            showToast('Error fulfilling reservation: ' + result.error, 'error');
        }
    });
}

// Cancel reservation
function cancelReservation(reservationId) {
    confirmDialog('Cancel this reservation?', async () => {
        const result = await ReservationsAPI.cancel(reservationId);
        
        if (result.success) {
            showToast('Reservation cancelled successfully!', 'success');
            loadReservationsPage();
        } else {
            showToast('Error cancelling reservation: ' + result.error, 'error');
        }
    });
}

// Search books with debounce
const searchBooks = debounce(async function(searchTerm) {
    if (!searchTerm) {
        loadBooksPage();
        return;
    }
    
    const result = await BooksAPI.search(searchTerm);
    
    if (result.success) {
        const container = document.getElementById('content-container');
        const columns = [
            { label: 'ISBN', field: 'isbn' },
            { label: 'Title', field: 'title' },
            { label: 'Author', field: 'author' },
            { label: 'Category', field: 'category' },
            { label: 'Available', field: 'availableCopies' },
            { label: 'Actions', field: 'isbn',
                formatter: (val) => `
                    <button class="btn btn-warning" onclick="editBook('${val}')">Edit</button>
                    <button class="btn btn-danger" onclick="deleteBook('${val}')">Delete</button>
                `
            }
        ];
        
        const tableHTML = createTable(result.data, columns);
        document.querySelector('.table-container').innerHTML = tableHTML;
    }
}, APP_CONFIG.debounceDelay);

// Setup mobile sidebar
function setupMobileSidebar() {
    document.addEventListener('click', function(e) {
        const sidebar = document.getElementById('sidebar');
        const toggleBtn = document.querySelector('.toggle-btn');
        
        if (window.innerWidth <= 768) {
            if (!sidebar.contains(e.target) && !toggleBtn.contains(e.target)) {
                sidebar.classList.remove('active');
            }
        }
    });
}