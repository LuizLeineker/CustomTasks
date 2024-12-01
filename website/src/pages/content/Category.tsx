import { Link } from "react-router-dom";

function Category() {
    return (
        <div>
            <h1>Seu Rótulos: talez seria uma boa ideia atribuir isso direto na  list label, aonde tem um botao para criar um novo rotulo!</h1>
            <ul>    
                <li>
                    <Link to="/label/create">Create Label</Link>
                </li>
                <li>
                    <Link to="/label/list/username">List Labels</Link>
                </li>
            </ul>
        </div>
    );
}

export default Category;