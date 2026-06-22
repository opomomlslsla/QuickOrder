import { useState, useEffect } from 'react';
import './App.css';
import type { Order, OrderFullInfo } from './types/order';

function App() {
    const [orders, setOrders] = useState<Order[]>([]);
    const [showForm, setShowForm] = useState(false);
    const [showViewModal, setShowViewModal] = useState(false);
    const [selectedOrder, setSelectedOrder] = useState<OrderFullInfo | null>(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const [formData, setFormData] = useState({
        senderCity: '',
        senderAddress: '',
        recipientCity: '',
        recipientAddress: '',
        weight: 0,
        pickupDate: ''
    });

    const [currentPage, setCurrentPage] = useState(1);

    const handleNextPage = () => {
        setCurrentPage(prev => prev + 1);
    };

    const handlePrevPage = () => {
        setCurrentPage(prev => prev - 1);
    };

    async function fetchOrders(p0: number) {
        try {
            setLoading(true);
            setError(null);
            const response = await fetch(`/api/orders?page=${p0}`);
            if (!response.ok) throw new Error('Failed to fetch');
            const data = await response.json();
            setOrders(data);
        } catch (error) {
            console.error('Error fetching orders:', error);
            setError('Ошибка при загрузке заказов');
        } finally {
            setLoading(false);
        }
    }

    useEffect(() => {
            fetchOrders(currentPage);}, [currentPage]);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            setLoading(true);
            const response = await fetch('/api/orders', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(formData)
            });
            if (response.ok) {
                setShowForm(false);
                await fetchOrders(currentPage);
                setFormData({
                    senderCity: '',
                    senderAddress: '',
                    recipientCity: '',
                    recipientAddress: '',
                    weight: 0,
                    pickupDate: ''
                });
            }
        } catch (error) {
            console.error('Error creating order:', error);
            setError('Ошибка при создании заказа');
        } finally {
            setLoading(false);
        }
    };

    const handleOrderClick = async (orderId: string) => {
        try {
            setLoading(true);
            const response = await fetch(`/api/orders/${orderId}`);
            if (!response.ok) throw new Error('Failed to fetch order');
            const order = await response.json();
            setSelectedOrder(order);
            setShowViewModal(true);
        } catch (error) {
            console.error('Error fetching order details:', error);
            setError('Ошибка при загрузке деталей заказа');
        } finally {
            setLoading(false);
        }
    };

    const closeViewModal = () => {
        setShowViewModal(false);
        setSelectedOrder(null);
    };

    const getStatusColor = (status: string) => {
        const colors: Record<string, string> = {
            'created': '#3498db',
            'in_progress': '#f39c12',
            'delivered': '#27ae60',
            'cancelled': '#e74c3c'
        };
        return colors[status] || '#95a5a6';
    };

    const getStatusLabel = (status: string) => {
        const labels: Record<string, string> = {
            'created': 'Создан',
            'in_progress': 'В пути',
            'delivered': 'Доставлен',
            'cancelled': 'Отменен'
        };
        return labels[status] || status;
    };

    const formatDate = (dateString: string) => {
        return new Date(dateString).toLocaleDateString('ru-RU', {
            day: '2-digit',
            month: '2-digit',
            year: 'numeric'
        });
    };

    return (
        <div className="app">
            <header className="app-header">
                <h1>📦 Заказы</h1>
                <button
                    className="btn-create"
                    onClick={() => setShowForm(!showForm)}
                >
                    {showForm ? '✕ Отмена' : '+ Создать заказ'}
                </button>
            </header>

            {error && (
                <div className="error-message">
                    {error}
                    <button onClick={() => setError(null)}>×</button>
                </div>
            )}

            {showForm && (
                <div className="modal-overlay" onClick={() => setShowForm(false)}>
                    <div className="modal" onClick={(e) => e.stopPropagation()}>
                        <div className="modal-header">
                            <h2>Создание нового заказа</h2>
                            <button className="modal-close" onClick={() => setShowForm(false)}>×</button>
                        </div>
                        <form onSubmit={handleSubmit} className="order-form">
                            <div className="form-group">
                                <label>Город отправителя *</label>
                                <input
                                    type="text"
                                    placeholder="Введите город отправителя"
                                    value={formData.senderCity}
                                    onChange={(e) => setFormData({ ...formData, senderCity: e.target.value })}
                                    required
                                />
                            </div>
                            <div className="form-group">
                                <label>Адрес отправителя *</label>
                                <input
                                    type="text"
                                    placeholder="Введите адрес отправителя"
                                    value={formData.senderAddress}
                                    onChange={(e) => setFormData({ ...formData, senderAddress: e.target.value })}
                                    required
                                />
                            </div>
                            <div className="form-group">
                                <label>Город получателя *</label>
                                <input
                                    type="text"
                                    placeholder="Введите город получателя"
                                    value={formData.recipientCity}
                                    onChange={(e) => setFormData({ ...formData, recipientCity: e.target.value })}
                                    required
                                />
                            </div>
                            <div className="form-group">
                                <label>Адрес получателя *</label>
                                <input
                                    type="text"
                                    placeholder="Введите адрес получателя"
                                    value={formData.recipientAddress}
                                    onChange={(e) => setFormData({ ...formData, recipientAddress: e.target.value })}
                                    required
                                />
                            </div>
                            <div className="form-group">
                                <label>Вес груза (кг) *</label>
                                <input
                                    type="number"
                                    placeholder="Введите вес груза"
                                    value={formData.weight || ''}
                                    onChange={(e) => setFormData({ ...formData, weight: parseFloat(e.target.value) || 0 })}
                                    min="0.1"
                                    step="0.1"
                                    required
                                />
                            </div>
                            <div className="form-group">
                                <label>Дата забора груза *</label>
                                <input
                                    type="date"
                                    value={formData.pickupDate}
                                    onChange={(e) => setFormData({ ...formData, pickupDate: e.target.value })}
                                    required
                                />
                            </div>
                            <div className="form-actions">
                                <button type="button" className="btn-cancel" onClick={() => setShowForm(false)}>
                                    Отмена
                                </button>
                                <button type="submit" className="btn-submit" disabled={loading}>
                                    {loading ? 'Создание...' : 'Создать заказ'}
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            )}

            {showViewModal && selectedOrder && (
                <div className="modal-overlay" onClick={closeViewModal}>
                    <div className="modal view-modal" onClick={(e) => e.stopPropagation()}>
                        <div className="modal-header">
                            <h2>Просмотр заказа</h2>
                            <button className="modal-close" onClick={closeViewModal}>×</button>
                        </div>

                        <div className="view-content">
                            <div className="view-row">
                                <span className="view-label">Серийный номер:</span>
                                <span className="view-value">№ {selectedOrder.serialNumber || selectedOrder.id.slice(0, 8)}</span>
                            </div>
                            <div className="view-row">
                                <span className="view-label">Город отправителя:</span>
                                <span className="view-value">{selectedOrder.senderCity}</span>
                            </div>
                            <div className="view-row">
                                <span className="view-label">Адрес отправителя:</span>
                                <span className="view-value">{selectedOrder.senderAddress}</span>
                            </div>
                            <div className="view-row">
                                <span className="view-label">Город получателя:</span>
                                <span className="view-value">{selectedOrder.recipientCity}</span>
                            </div>
                            <div className="view-row">
                                <span className="view-label">Адрес получателя:</span>
                                <span className="view-value">{selectedOrder.recipientAddress}</span>
                            </div>
                            <div className="view-row">
                                <span className="view-label">Вес груза:</span>
                                <span className="view-value">{selectedOrder.weight} кг</span>
                            </div>
                            <div className="view-row">
                                <span className="view-label">Дата забора:</span>
                                <span className="view-value">{formatDate(selectedOrder.pickupDate)}</span>
                            </div>
                            <div className="view-row">
                                <span className="view-label">Статус:</span>
                                <span className="view-value">
                                    <span
                                        className="status-badge"
                                        style={{ backgroundColor: getStatusColor(selectedOrder.status) }}
                                    >
                                        {getStatusLabel(selectedOrder.status)}
                                    </span>
                                </span>
                            </div>
                        </div>

                        <div className="view-actions">
                            <button className="btn-cancel" onClick={closeViewModal}>
                                Закрыть
                            </button>
                        </div>
                    </div>
                </div>
            )}

            <div className="orders-container">
                <h2 className="section-title">Список заказов</h2>

                <button className="btn-create" onClick={handlePrevPage}
                    disabled={currentPage === 1 || loading}>
                    ◀
                </button>

                <span className="page-info">
                    Страница <strong>{currentPage}</strong>
                </span>

                <button className="btn-create" onClick={handleNextPage} disabled={loading || orders.length < 100}>
                    ▶
                </button>

                {loading && <div className="loading">Загрузка...</div>}

                {!loading && orders.length === 0 && (
                    <div className="empty-state">Нет созданных заказов</div>
                )}

                {!loading && orders.length > 0 && (
                    <div className="table-wrapper">
                        <table className="orders-table">
                            <thead>
                                <tr>
                                    <th>Серийный номер</th>
                                    <th>Дата забора</th>
                                    <th>Город получателя</th>
                                    <th>Город отправителя</th>
                                    <th>Статус</th>
                                </tr>
                            </thead>
                            <tbody>
                                {orders.map((order) => (
                                    <tr
                                        key={order.id}
                                        className="order-row"
                                        onClick={() => handleOrderClick(order.id)}
                                    >
                                        <td className="serial-number">{order.serialNumber || order.id.slice(0, 8)}</td>
                                        <td>{formatDate(order.pickupDate)}</td>
                                        <td>{order.recipientCity}</td>
                                        <td>{order.senderCity}</td>
                                        <td>
                                            <span
                                                className="status-badge"
                                                style={{ backgroundColor: getStatusColor(order.status) }}
                                            >
                                                {getStatusLabel(order.status)}
                                            </span>
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                )}
            </div>
        </div>
    );
}

export default App;