import javax.swing.*;

public class App {

    private JButton button1;
    private JPanel panel1;

    public static void main(String[] args) {
        JFrame frame=new JFrame("App");
        frame.setContentPane(new App().panel1);
        frame.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
        frame.pack();
        frame.setVisible(true);
    }

}
