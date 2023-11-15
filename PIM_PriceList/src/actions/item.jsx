import api from "./api";

export const ACTION_TYPES = {
    CREATE: 'CREATE',
    UPDATE: 'UPDATE',
    DELETE: 'DELETE',
    FETCH_ALL: 'FETCH_ALL'

}
// Since age is initialized with '' here and is integer in Db, so if it's an empty(null) string, make equal to 0, otherwise let it be unchanged
const FormatData = data => ({
    ...data,
    itemRetailPrice: parseFloat(data.itemRetailPrice ? data.itemRetailPrice : 0)
})

// fetchAll action creator
export const fetchAll = () => dispatch => {
    api.ItemRegister().fetchAll()
        .then(response => {
            dispatch({
                type: ACTION_TYPES.FETCH_ALL,
                payload: response.data
            })
        })
        .catch(err => console.log(err))
}

export const create = (data, onSuccess) => dispatch => {
    data = FormatData(data)
    api.ItemRegister().create(data)
        .then(response => {
            dispatch({
                type: ACTION_TYPES.CREATE,
                payload: response.data
            })
            onSuccess()
        })
        .catch(err => console.log(err))
}

export const update = (itemName, data, onSuccess) => dispatch => {
    data = FormatData(data)
    api.ItemRegister().update(itemName, data)
        .then(response => {
            dispatch({
                type: ACTION_TYPES.UPDATE,
                payload: { itemName, ...data }
            })
            onSuccess()
        })
        .catch(err => console.log(err))
}
export const Delete = (itemName, data, onSuccess) => dispatch => {
    api.ItemRegister().delete(itemName)
        .then(response => {
            dispatch({
                type: ACTION_TYPES.DELETE,
                payload: itemName
            })
            onSuccess()
        })
        .catch(err => console.log(err))
}