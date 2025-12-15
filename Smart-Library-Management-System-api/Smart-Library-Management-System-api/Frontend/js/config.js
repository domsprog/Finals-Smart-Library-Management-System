
const API_BASE_URL = 'http://localhost:5000/api';

const API_ENDPOINTS = {

    books: {
        getAll: `${API_BASE_URL}/books`,
        getByISBN: (isbn) => `${API_BASE_URL}/books/${isbn}`,
        create: `${API_BASE_URL}/books/create`,
        update: (isbn) => `${API_BASE_URL}/books/update/${isbn}`,
        delete: (isbn) => `${API_BASE_URL}/books/delete/${isbn}`,
        search: (term) => `${API_BASE_URL}/books/search/${term}`,
        checkAvailability: (isbn) => `${API_BASE_URL}/books/available/${isbn}`
    },
    

    users: {
        getAll: `${API_BASE_URL}/user`,
        getById: (id) => `${API_BASE_URL}/user/${id}`,
        registerStudent: `${API_BASE_URL}/user/student`,
        registerFaculty: `${API_BASE_URL}/user/faculty`,
        update: (id) => `${API_BASE_URL}/user/${id}`,
        delete: (id) => `${API_BASE_URL}/user/${id}`
    },
    

    loans: {
        getAll: `${API_BASE_URL}/loans`,
        getById: (id) => `${API_BASE_URL}/loans/${id}`,
        getByUser: (userId) => `${API_BASE_URL}/loans/user/${userId}`,
        getOverdue: `${API_BASE_URL}/loans/overdue`,
        borrow: `${API_BASE_URL}/loans/borrow`,
        return: `${API_BASE_URL}/loans/return`
    },
    

    fines: {
        getAll: `${API_BASE_URL}/fine`,
        getById: (id) => `${API_BASE_URL}/fine/${id}`,
        getByUser: (userId) => `${API_BASE_URL}/fine/user/${userId}`,
        getByLoan: (loanId) => `${API_BASE_URL}/fine/loan/${loanId}`,
        create: `${API_BASE_URL}/fine/create`,
        pay: (id) => `${API_BASE_URL}/fine/pay/${id}`,
        update: (id) => `${API_BASE_URL}/fine/${id}`
    },
    

    reservations: {
        getAll: `${API_BASE_URL}/reservations`,
        getById: (id) => `${API_BASE_URL}/reservations/${id}`,
        getByUser: (userId) => `${API_BASE_URL}/reservations/user/${userId}`,
        getActive: `${API_BASE_URL}/reservations/active`,
        create: `${API_BASE_URL}/reservations`,
        cancel: (id) => `${API_BASE_URL}/reservations/cancel/${id}`,
        fulfill: (id) => `${API_BASE_URL}/reservations/fulfill/${id}`
    },
    

    catalog: {
        getAll: `${API_BASE_URL}/catalog`,
        getById: (id) => `${API_BASE_URL}/catalog/${id}`,
        create: `${API_BASE_URL}/catalog`,
        update: (id) => `${API_BASE_URL}/catalog/${id}`,
        delete: (id) => `${API_BASE_URL}/catalog/${id}`
    }
};

// App Configuration
const APP_CONFIG = {
    itemsPerPage: 10,
    toastDuration: 3000, 
    debounceDelay: 500 
};