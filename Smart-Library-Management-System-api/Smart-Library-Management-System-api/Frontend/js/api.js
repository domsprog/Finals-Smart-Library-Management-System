// Generic API call function
async function apiCall(url, options = {}) {
    try {
        // Show loading spinner
        showLoading();
        
        // Set default headers
        const defaultOptions = {
            headers: {
                'Content-Type': 'application/json',
            },
            ...options
        };
        
        // Make the API call
        const response = await fetch(url, defaultOptions);
        
        // Hide loading spinner
        hideLoading();
        
        // Check if response is OK
        if (!response.ok) {
            const error = await response.text();
            throw new Error(error || `HTTP error! status: ${response.status}`);
        }
        
        // Parse JSON response
        const data = await response.json();
        return { success: true, data };
        
    } catch (error) {
        hideLoading();
        console.error('API Error:', error);
        return { success: false, error: error.message };
    }
}

// BOOKS API 
const BooksAPI = {
    // Get all books
    async getAll() {
        return await apiCall(API_ENDPOINTS.books.getAll);
    },
    
    // Get book by ISBN
    async getByISBN(isbn) {
        return await apiCall(API_ENDPOINTS.books.getByISBN(isbn));
    },
    
    // Create new book
    async create(bookData) {
        return await apiCall(API_ENDPOINTS.books.create, {
            method: 'POST',
            body: JSON.stringify(bookData)
        });
    },
    
    // Update book
    async update(isbn, bookData) {
        return await apiCall(API_ENDPOINTS.books.update(isbn), {
            method: 'PUT',
            body: JSON.stringify(bookData)
        });
    },
    
    // Delete book
    async delete(isbn) {
        return await apiCall(API_ENDPOINTS.books.delete(isbn), {
            method: 'DELETE'
        });
    },
    
    // Search books
    async search(term) {
        return await apiCall(API_ENDPOINTS.books.search(term));
    }
};

// USERS API
const UsersAPI = {
    // Get all users
    async getAll() {
        return await apiCall(API_ENDPOINTS.users.getAll);
    },
    
    // Get user by ID
    async getById(id) {
        return await apiCall(API_ENDPOINTS.users.getById(id));
    },
    
    // Register student
    async registerStudent(studentData) {
        return await apiCall(API_ENDPOINTS.users.registerStudent, {
            method: 'POST',
            body: JSON.stringify(studentData)
        });
    },
    
    // Register faculty
    async registerFaculty(facultyData) {
        return await apiCall(API_ENDPOINTS.users.registerFaculty, {
            method: 'POST',
            body: JSON.stringify(facultyData)
        });
    },
    
    // Update user
    async update(id, userData) {
        return await apiCall(API_ENDPOINTS.users.update(id), {
            method: 'PUT',
            body: JSON.stringify(userData)
        });
    },
    
    // Delete user
    async delete(id) {
        return await apiCall(API_ENDPOINTS.users.delete(id), {
            method: 'DELETE'
        });
    }
};

// LOANS API 
const LoansAPI = {
    // Get all loans
    async getAll() {
        return await apiCall(API_ENDPOINTS.loans.getAll);
    },
    
    // Get loan by ID
    async getById(id) {
        return await apiCall(API_ENDPOINTS.loans.getById(id));
    },
    
    // Get loans by user
    async getByUser(userId) {
        return await apiCall(API_ENDPOINTS.loans.getByUser(userId));
    },
    
    // Get overdue loans
    async getOverdue() {
        return await apiCall(API_ENDPOINTS.loans.getOverdue);
    },
    
    // Borrow book
    async borrow(loanData) {
        return await apiCall(API_ENDPOINTS.loans.borrow, {
            method: 'POST',
            body: JSON.stringify(loanData)
        });
    },
    
    // Return book
    async return(loanData) {
        return await apiCall(API_ENDPOINTS.loans.return, {
            method: 'POST',
            body: JSON.stringify(loanData)
        });
    }
};

// FINES API 
const FinesAPI = {
    // Get all fines
    async getAll() {
        return await apiCall(API_ENDPOINTS.fines.getAll);
    },
    
    // Get fine by ID
    async getById(id) {
        return await apiCall(API_ENDPOINTS.fines.getById(id));
    },
    
    // Get fines by user
    async getByUser(userId) {
        return await apiCall(API_ENDPOINTS.fines.getByUser(userId));
    },
    
    // Create fine
    async create(fineData) {
        return await apiCall(API_ENDPOINTS.fines.create, {
            method: 'POST',
            body: JSON.stringify(fineData)
        });
    },
    
    // Pay fine
    async pay(id) {
        return await apiCall(API_ENDPOINTS.fines.pay(id), {
            method: 'PUT'
        });
    }
};

// RESERVATIONS API
const ReservationsAPI = {
    // Get all reservations
    async getAll() {
        return await apiCall(API_ENDPOINTS.reservations.getAll);
    },
    
    // Get reservation by ID
    async getById(id) {
        return await apiCall(API_ENDPOINTS.reservations.getById(id));
    },
    
    // Get reservations by user
    async getByUser(userId) {
        return await apiCall(API_ENDPOINTS.reservations.getByUser(userId));
    },
    
    // Get active reservations
    async getActive() {
        return await apiCall(API_ENDPOINTS.reservations.getActive);
    },
    
    // Create reservation
    async create(reservationData) {
        return await apiCall(API_ENDPOINTS.reservations.create, {
            method: 'POST',
            body: JSON.stringify(reservationData)
        });
    },
    
    // Cancel reservation
    async cancel(id) {
        return await apiCall(API_ENDPOINTS.reservations.cancel(id), {
            method: 'PUT'
        });
    },
    
    // Fulfill reservation
    async fulfill(id) {
        return await apiCall(API_ENDPOINTS.reservations.fulfill(id), {
            method: 'PUT'
        });
    }
};
